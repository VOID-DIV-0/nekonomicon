namespace Nekonomicon.Core.Parser.Ast;

/// <summary>
/// Literal type for values in the AST.
/// </summary>
public enum LiteralType
{
    String,
    Number,
    Boolean
}

/// <summary>
/// Represents a literal value in the AST.
/// </summary>
public class LiteralNode : AstNode
{
    /// <summary>
    /// Type of the literal (String, Number, Boolean).
    /// </summary>
    public LiteralType Type { get; }

    /// <summary>
    /// The actual value (string, int, double, or bool).
    /// </summary>
    public object Value { get; }

    public LiteralNode(LiteralType type, object value, int line, int column)
        : base(line, column)
    {
        if (value == null)
            throw new ArgumentException("Value must not be null", nameof(value));

        Type = type;
        Value = value;
    }
}
