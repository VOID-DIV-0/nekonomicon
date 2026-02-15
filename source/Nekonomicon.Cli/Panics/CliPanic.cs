using Nekonomicon.Core.Models;

namespace Nekonomicon.Cli.Panics;

public record CliPanic : Failure
{
    public required CliPanicCode Code { get; init; }
    
    /// <summary>
    /// Optional: Additional context for educative formatting
    /// </summary>
    public string? Why { get; init; }
    
    /// <summary>
    /// Optional: Suggestions for how to fix the error
    /// </summary>
    public List<string>? Suggestions { get; init; }
    
    /// <summary>
    /// Optional: Code snippet or context where the error occurred
    /// </summary>
    public string? Context { get; init; }

    public static CliPanic Create(string message, CliPanicCode code = CliPanicCode.InvalidInput)
    {
        return new()
        {
            Code = code,
            Message = message
        };
    }

    public static CliPanic Create(
        string message, 
        CliPanicCode code, 
        string? why = null, 
        List<string>? suggestions = null,
        string? context = null)
    {
        return new()
        {
            Code = code,
            Message = message,
            Why = why,
            Suggestions = suggestions,
            Context = context
        };
    }
}
