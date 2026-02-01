namespace Nekonomicon.Core.Parser.Ast;

/// <summary>
/// Represents a reference to a variable in the AST.
/// </summary>
public class VariableReferenceNode : AstNode
{
    /// <summary>
    /// Variable name without @ prefix.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Type modifier (Mutable, Constant, Nullable).
    /// </summary>
    public VariableModifier Modifier { get; }

    public VariableReferenceNode(string name, VariableModifier modifier, int line, int column)
        : base(line, column)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name must not be empty", nameof(name));

        Name = name;
        Modifier = modifier;
    }
}
