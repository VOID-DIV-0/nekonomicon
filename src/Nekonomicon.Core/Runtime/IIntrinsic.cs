namespace Nekonomicon.Core.Runtime;

/// <summary>
/// Represents a builtin intrinsic function/command that can be executed.
/// </summary>
public interface IIntrinsic
{
    /// <summary>
    /// Executes the intrinsic with the given arguments in the provided context.
    /// </summary>
    Result<Unit> Execute(List<object?> arguments, ExecutionContext context);
}
