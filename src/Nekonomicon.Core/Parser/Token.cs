namespace Nekonomicon.Core.Parser;

/// <summary>
/// Represents a single token from the lexer.
/// </summary>
public record Token(
    TokenType Type,
    string Value,
    int Line,
    int Column
);

