namespace Nekonomicon.Core.Parser;

using System.Text;

/// <summary>
/// Lexer for tokenizing nekonomicon scripts.
/// Converts raw text into a stream of tokens.
/// </summary>
public class Lexer
{
    private static readonly HashSet<string> Keywords = new()
    {
        "set", "say", "decide", "invoke", "if", "then", "else", "end",
        "true", "false", "safe", "elevated", "sensitive", "async"
    };

    private static readonly Dictionary<string, TokenType> Operators = new()
    {
        { "to", TokenType.To },
        { "into", TokenType.Into },
        { "with", TokenType.With },
        { "without", TokenType.Without },
        { "is", TokenType.Is },
        { "greater", TokenType.Greater },
        { "less", TokenType.Less },
        { "than", TokenType.Than },
        { "equal", TokenType.Equal },
        { "not", TokenType.Not }
    };

    /// <summary>
    /// Tokenizes a script string into a list of tokens.
    /// </summary>
    public List<Token> Tokenize(string script)
    {
        var tokens = new List<Token>();
        var normalizedScript = NormalizeLineEndings(script);
        
        int line = 1;
        int column = 1;
        int pos = 0;

        while (pos < normalizedScript.Length)
        {
            int ch = normalizedScript[pos];
            
            // Skip whitespace (except newlines which we track)
            if (char.IsWhiteSpace((char)ch) && ch != '\n')
            {
                pos++;
                column++;
                continue;
            }

            // Handle newlines
            if (ch == '\n')
            {
                pos++;
                line++;
                column = 1;
                continue;
            }

            // Handle comments
            if (ch == '#')
            {
                if (pos + 1 < normalizedScript.Length && normalizedScript[pos + 1] == '#')
                {
                    // Multi-line comment
                    pos += 2; // consume ##
                    column += 2;

                    while (pos < normalizedScript.Length)
                    {
                        if (normalizedScript[pos] == '#' && pos + 1 < normalizedScript.Length && normalizedScript[pos + 1] == '#')
                        {
                            pos += 2;
                            column += 2;
                            break;
                        }
                        if (normalizedScript[pos] == '\n')
                        {
                            line++;
                            column = 1;
                        }
                        else
                        {
                            column++;
                        }
                        pos++;
                    }
                }
                else
                {
                    // Single-line comment
                    while (pos < normalizedScript.Length && normalizedScript[pos] != '\n')
                    {
                        pos++;
                        column++;
                    }
                }
                continue;
            }

            // Handle strings
            if (ch == '"')
            {
                var startLine = line;
                var startCol = column;
                var (value, length) = ReadString(normalizedScript, ref pos, ref line, ref column);
                tokens.Add(new Token(TokenType.StringLiteral, value, startLine, startCol));
                continue;
            }

            // Handle numbers
            if (char.IsDigit((char)ch))
            {
                var startLine = line;
                var startCol = column;
                var (value, length) = ReadNumber(normalizedScript, ref pos);
                tokens.Add(new Token(TokenType.NumberLiteral, value, startLine, startCol));
                column += length;
                continue;
            }

            // Handle variables (@name, @!const, @?nullable)
            if (ch == '@')
            {
                var startLine = line;
                var startCol = column;
                var (value, length) = ReadVariable(normalizedScript, ref pos);
                tokens.Add(new Token(TokenType.Identifier, value, startLine, startCol));
                column += length;
                continue;
            }

            // Handle period
            if (ch == '.')
            {
                tokens.Add(new Token(TokenType.Period, ".", line, column));
                pos++;
                column++;
                continue;
            }

            // Handle identifiers and keywords
            if (char.IsLetter((char)ch) || ch == '_')
            {
                var startLine = line;
                var startCol = column;
                var (value, length) = ReadIdentifier(normalizedScript, ref pos);
                var type = DetermineTokenType(value);
                tokens.Add(new Token(type, value, startLine, startCol));
                column += length;
                continue;
            }

            // Unknown character - skip
            pos++;
            column++;
        }

        // Add EOF token
        tokens.Add(new Token(TokenType.EndOfFile, "", line, column));
        return tokens;
    }

    private static string NormalizeLineEndings(string script)
    {
        return script.Replace("\r\n", "\n");
    }

    private static (string value, int length) ReadString(string script, ref int pos, ref int line, ref int column)
    {
        var sb = new StringBuilder();
        pos++; // consume opening quote
        column++;

        while (pos < script.Length && script[pos] != '"')
        {
            if (script[pos] == '\n')
            {
                line++;
                column = 1;
            }
            else
            {
                column++;
            }
            sb.Append(script[pos]);
            pos++;
        }

        if (pos < script.Length && script[pos] == '"')
        {
            pos++; // consume closing quote
            column++;
        }

        return (sb.ToString(), sb.Length + 2); // +2 for quotes
    }

    private static (string value, int length) ReadNumber(string script, ref int pos)
    {
        var sb = new StringBuilder();
        bool hasDecimal = false;

        while (pos < script.Length)
        {
            if (char.IsDigit(script[pos]))
            {
                sb.Append(script[pos]);
                pos++;
            }
            else if (script[pos] == '.' && !hasDecimal && pos + 1 < script.Length && char.IsDigit(script[pos + 1]))
            {
                hasDecimal = true;
                sb.Append(script[pos]);
                pos++;
            }
            else
            {
                break;
            }
        }

        return (sb.ToString(), sb.Length);
    }

    private static (string value, int length) ReadVariable(string script, ref int pos)
    {
        var sb = new StringBuilder();
        sb.Append(script[pos]); // consume @
        pos++;

        // Check for modifiers
        if (pos < script.Length && (script[pos] == '!' || script[pos] == '?'))
        {
            sb.Append(script[pos]);
            pos++;
        }

        // Read variable name
        while (pos < script.Length && (char.IsLetterOrDigit(script[pos]) || script[pos] == '_'))
        {
            sb.Append(script[pos]);
            pos++;
        }

        return (sb.ToString(), sb.Length);
    }

    private static (string value, int length) ReadIdentifier(string script, ref int pos)
    {
        var sb = new StringBuilder();

        while (pos < script.Length && (char.IsLetterOrDigit(script[pos]) || script[pos] == '_'))
        {
            sb.Append(script[pos]);
            pos++;
        }

        return (sb.ToString(), sb.Length);
    }

    private static TokenType DetermineTokenType(string value)
    {
        if (Keywords.Contains(value))
            return TokenType.Keyword;
        
        if (Operators.TryGetValue(value, out var opType))
            return opType;
        
        if (value is "true" or "false")
            return TokenType.BooleanLiteral;
        
        return TokenType.Identifier;
    }
}
