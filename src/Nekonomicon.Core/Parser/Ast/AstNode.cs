namespace Nekonomicon.Core.Parser.Ast;

/// <summary>
/// Base class for all Abstract Syntax Tree nodes.
/// </summary>
public abstract class AstNode
{
    /// <summary>
    /// Source line number (1-indexed).
    /// </summary>
    public int Line { get; }

    /// <summary>
    /// Source column number (1-indexed).
    /// </summary>
    public int Column { get; }

    protected AstNode(int line, int column)
    {
        if (line <= 0)
            throw new ArgumentException("Line must be greater than 0", nameof(line));
        if (column <= 0)
            throw new ArgumentException("Column must be greater than 0", nameof(column));

        Line = line;
        Column = column;
    }
}
