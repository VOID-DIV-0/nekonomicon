using System.Text;
using Nekonomicon.Core.Parser;
using Nekonomicon.Core.Parser.Ast;

namespace Nekonomicon.Core.Runtime;

/// <summary>
/// Orchestrates lexing, parsing, and execution of nekonomicon scripts.
/// </summary>
public class ScriptEngine
{
    private readonly ExecutionContext _context;
    private readonly Lexer _lexer;
    private readonly RuntimeParser _parser;
    private readonly Dictionary<string, IIntrinsic> _intrinsics;
    private readonly StringBuilder _output;

    public ScriptEngine()
    {
        _context = new ExecutionContext();
        _lexer = new Lexer();
        _parser = new RuntimeParser();
        _intrinsics = new Dictionary<string, IIntrinsic>(StringComparer.OrdinalIgnoreCase)
        {
            { "set", new SetIntrinsic() },
            { "say", new SayIntrinsic() }
        };
        _output = new StringBuilder();
    }

    /// <summary>
    /// Executes a script and returns the result.
    /// </summary>
    public Result<ScriptResult> Execute(string script)
    {
        try
        {
            // Lexing phase
            var tokenList = _lexer.Tokenize(script);
            var tokens = tokenList.ToArray();

            // Parsing phase
            List<CommandNode> commands;
            try
            {
                commands = _parser.ParseMultiple(tokens);
            }
            catch (Exception ex)
            {
                return Result<ScriptResult>.Failure(ex.Message, ex);
            }

            // Execution phase
            foreach (var command in commands)
            {
                var execResult = ExecuteCommand(command);
                if (!execResult.IsSuccess)
                {
                    return Result<ScriptResult>.Failure(execResult.ErrorMessage);
                }
            }

            return Result<ScriptResult>.Success(new ScriptResult
            {
                IsSuccess = true,
                Output = _output.ToString().TrimEnd(),
                ErrorMessage = ""
            });
        }
        catch (Exception ex)
        {
            return Result<ScriptResult>.Failure($"Runtime error: {ex.Message}", ex);
        }
    }

    private Result<Unit> ExecuteCommand(CommandNode command)
    {
        if (!_intrinsics.TryGetValue(command.Intrinsic, out var intrinsic))
        {
            return Result<Unit>.Failure($"Unknown command: {command.Intrinsic}");
        }

        // Special handling for "set" command - need variable name, not value
        if (command.Intrinsic.Equals("set", StringComparison.OrdinalIgnoreCase))
        {
            if (command.Arguments.Length < 2)
            {
                return Result<Unit>.Failure("set requires 2 arguments");
            }

            // First argument is variable reference
            if (!(command.Arguments[0] is VariableReferenceNode varRef))
            {
                return Result<Unit>.Failure("set requires variable name as first argument");
            }

            // Second argument is the value
            var valueExpr = EvaluateExpression(command.Arguments[1]);
            if (!valueExpr.IsSuccess)
            {
                return Result<Unit>.Failure(valueExpr.ErrorMessage);
            }

            // Call SetIntrinsic with variable name, modifier, and value
            var args = new List<object?> { varRef.Name, varRef.Modifier, valueExpr.Value };
            return intrinsic.Execute(args, _context);
        }

        // For other commands, evaluate all arguments
        var evaluatedArgs = new List<object?>();
        foreach (var arg in command.Arguments)
        {
            var evaluated = EvaluateExpression(arg);
            if (!evaluated.IsSuccess)
            {
                return Result<Unit>.Failure(evaluated.ErrorMessage);
            }
            evaluatedArgs.Add(evaluated.Value);
        }

        // Execute intrinsic
        var result = intrinsic.Execute(evaluatedArgs, _context);
        if (!result.IsSuccess)
        {
            return result;
        }

        // Capture output from intrinsic
        if (intrinsic is SayIntrinsic say)
        {
            var output = say.GetLastOutput();
            if (output != null)
            {
                if (_output.Length > 0)
                {
                    _output.AppendLine();
                }
                _output.Append(output);
            }
        }

        return result;
    }

    private Result<object?> EvaluateExpression(AstNode expr)
    {
        if (expr is LiteralNode literal)
        {
            return Result<object?>.Success(literal.Value);
        }

        if (expr is VariableReferenceNode varRef)
        {
            var result = _context.GetVariable(varRef.Name);
            if (!result.IsSuccess)
            {
                return Result<object?>.Failure(result.ErrorMessage);
            }
            return Result<object?>.Success(result.Value!.Value);
        }

        return Result<object?>.Failure($"Unknown expression type: {expr.GetType().Name}");
    }
}
