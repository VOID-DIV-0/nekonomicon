namespace Nekonomicon.Core.Parser;

/// <summary>
/// Types of lexical tokens in nekonomicon scripts.
/// </summary>
public enum TokenType
{
    // Keywords
    Keyword,

    // Identifiers and variables
    Identifier,

    // Literals
    StringLiteral,
    NumberLiteral,
    BooleanLiteral,

    // Operators and symbols
    Symbol,
    To,
    Into,
    With,
    Without,
    Is,
    Greater,
    Less,
    Than,
    Equal,
    Not,

    // Terminators and delimiters
    Period,
    Newline,

    // Special
    Comment,
    EndOfFile,
}
