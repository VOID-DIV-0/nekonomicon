namespace Nekonomicon.Core.Parser.Ast;

/// <summary>
/// Modifier for variable types.
/// </summary>
public enum VariableModifier
{
    /// <summary>
    /// Mutable variable (@name).
    /// </summary>
    Mutable,

    /// <summary>
    /// Constant variable (@!name) - cannot be reassigned.
    /// </summary>
    Constant,

    /// <summary>
    /// Nullable variable (@?name) - can be null.
    /// </summary>
    Nullable
}
