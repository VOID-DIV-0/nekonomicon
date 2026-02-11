namespace Nekonomicon.Core.Models;

/// <summary>
/// Represents a successful result with no value
/// </summary>
public record Ok
{
    public static Ok Value { get; } = new Ok();
}
