namespace Nekonomicon.Core.Parser.Ast;

/// <summary>
/// Represents a command statement in the AST.
/// </summary>
public class CommandNode : AstNode
{
    /// <summary>
    /// Command name (e.g., "set", "say", "decide").
    /// </summary>
    public string Intrinsic { get; }

    /// <summary>
    /// Command arguments.
    /// </summary>
    public AstNode[] Arguments { get; }

    public CommandNode(string intrinsic, AstNode[] arguments, int line, int column)
        : base(line, column)
    {
        if (string.IsNullOrEmpty(intrinsic))
            throw new ArgumentException("Intrinsic must not be empty", nameof(intrinsic));

        Intrinsic = intrinsic;
        Arguments = arguments ?? Array.Empty<AstNode>();
    }
}
