using Nekonomicon.Core.Parser;
using Nekonomicon.Core.Parser.Ast;
using Nekonomicon.Core.Runtime;

namespace Nekonomicon.Core.Runtime;

/// <summary>
/// Parses tokens into an Abstract Syntax Tree (AST) for execution.
/// </summary>
public class RuntimeParser
{
    private Token[] _tokens = null!;
    private int _current = 0;

    /// <summary>
    /// Parses a single command from tokens. Used for testing.
    /// </summary>
    public Result<AstNode> Parse(Token[] tokens)
    {
        _tokens = tokens;
        _current = 0;

        // Skip leading newlines and comments
        SkipTrivia();

        // If we hit EOF immediately, that's an error
        if (IsAtEnd())
        {
            return Result<AstNode>.Failure("Empty script");
        }

        // Parse the first command
        var commandResult = ParseCommand();
        if (!commandResult.IsSuccess)
        {
            return commandResult;
        }

        return commandResult;
    }

    /// <summary>
    /// Parses a sequence of tokens into an AST.
    /// Returns a list of command nodes.
    /// </summary>
    public List<CommandNode> ParseMultiple(Token[] tokens)
    {
        _tokens = tokens;
        _current = 0;
        var commands = new List<CommandNode>();

        while (!IsAtEnd())
        {
            // Skip trivia
            SkipTrivia();

            if (IsAtEnd())
                break;

            // Parse the command
            var commandResult = ParseCommand();
            if (!commandResult.IsSuccess)
            {
                throw new InvalidOperationException($"Parse error: {commandResult.ErrorMessage}");
            }

            var command = commandResult.Value as CommandNode;
            if (command != null)
            {
                commands.Add(command);
            }
        }

        return commands;
    }

    private Result<AstNode> ParseCommand()
    {
        // Get the command name (intrinsic)
        if (!Check(TokenType.Keyword))
        {
            return Result<AstNode>.Failure($"Expected keyword, got {Peek().Value}");
        }

        var commandName = Advance().Value.ToLower();
        var commandLine = Peek().Line;
        var commandColumn = Peek().Column;

        // Parse arguments based on command type
        var arguments = new List<AstNode>();

        if (commandName == "set")
        {
            // set @variable to <value>.
            var varResult = ParseVariableReference();
            if (!varResult.IsSuccess) return varResult;
            arguments.Add(varResult.Value!);

            // Expect "to" operator
            if (!Check(TokenType.To))
            {
                return Result<AstNode>.Failure("Expected 'to' after variable in set command");
            }
            Advance();

            // Parse value
            var valueResult = ParseExpression();
            if (!valueResult.IsSuccess) return valueResult;
            arguments.Add(valueResult.Value!);
        }
        else if (commandName == "say")
        {
            // say <expression>.
            var exprResult = ParseExpression();
            if (!exprResult.IsSuccess) return exprResult;
            arguments.Add(exprResult.Value!);
        }
        else
        {
            return Result<AstNode>.Failure($"Unknown command: {commandName}");
        }

        // Expect period terminator
        if (!Check(TokenType.Period))
        {
            return Result<AstNode>.Failure($"Expected period terminator at end of command, got {Peek().Value}");
        }
        Advance();

        return Result<AstNode>.Success(
            new CommandNode(commandName, arguments.ToArray(), commandLine, commandColumn)
        );
    }

    private Result<AstNode> ParseExpression()
    {
        var token = Peek();

        return token.Type switch
        {
            TokenType.StringLiteral => ParseStringLiteral(),
            TokenType.NumberLiteral => ParseNumberLiteral(),
            TokenType.BooleanLiteral => ParseBooleanLiteral(),
            TokenType.Identifier when token.Value.StartsWith("@") => ParseVariableReference(),
            _ => Result<AstNode>.Failure($"Unexpected token in expression: {token.Value}")
        };
    }

    private Result<AstNode> ParseStringLiteral()
    {
        var token = Advance();
        var value = token.Value;

        // Remove quotes if present
        if (value.StartsWith("\"") && value.EndsWith("\""))
        {
            value = value.Substring(1, value.Length - 2);
        }

        return Result<AstNode>.Success(
            new LiteralNode(LiteralType.String, value, token.Line, token.Column)
        );
    }

    private Result<AstNode> ParseNumberLiteral()
    {
        var token = Advance();
        if (!double.TryParse(token.Value, out var number))
        {
            return Result<AstNode>.Failure($"Invalid number: {token.Value}");
        }

        return Result<AstNode>.Success(
            new LiteralNode(LiteralType.Number, number, token.Line, token.Column)
        );
    }

    private Result<AstNode> ParseBooleanLiteral()
    {
        var token = Advance();
        var value = token.Value.ToLower() == "true";

        return Result<AstNode>.Success(
            new LiteralNode(LiteralType.Boolean, value, token.Line, token.Column)
        );
    }

    private Result<AstNode> ParseVariableReference()
    {
        if (!Check(TokenType.Identifier) || !Peek().Value.StartsWith("@"))
        {
            return Result<AstNode>.Failure($"Expected variable reference, got {Peek().Value}");
        }

        var token = Advance();
        var name = token.Value;

        // Remove @ prefix first
        if (name.StartsWith("@"))
        {
            name = name.Substring(1);
        }

        // Parse modifiers
        var modifier = VariableModifier.Mutable;
        if (name.Contains("!"))
        {
            modifier = VariableModifier.Constant;
            name = name.Replace("!", "");
        }
        else if (name.Contains("?"))
        {
            modifier = VariableModifier.Nullable;
            name = name.Replace("?", "");
        }

        return Result<AstNode>.Success(
            new VariableReferenceNode(name, modifier, token.Line, token.Column)
        );
    }

    private void SkipTrivia()
    {
        while (!IsAtEnd())
        {
            var token = Peek();
            if (token.Type == TokenType.Newline || token.Type == TokenType.Comment)
            {
                Advance();
            }
            else
            {
                break;
            }
        }
    }

    private bool Check(TokenType type)
    {
        if (IsAtEnd()) return false;
        return Peek().Type == type;
    }

    private Token Advance()
    {
        if (!IsAtEnd()) _current++;
        return Previous();
    }

    private bool IsAtEnd()
    {
        return Peek().Type == TokenType.EndOfFile;
    }

    private Token Peek()
    {
        return _tokens[_current];
    }

    private Token Previous()
    {
        return _tokens[_current - 1];
    }
}
