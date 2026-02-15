namespace Nekonomicon.Core.Runtime;

/// <summary>
/// Represents the overall result of script execution.
/// </summary>
public record ScriptResult
{
    public required bool IsSuccess { get; init; }
    public required string Output { get; init; }
    public required string ErrorMessage { get; init; }
}
