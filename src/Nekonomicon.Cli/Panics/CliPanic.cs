using Nekonomicon.Core.Models.Result;

namespace Nekonomicon.Cli.Panics;

public enum CliPanicCode
{
    InvalidInput,
}

public record CliPanic(CliPanicCode ErrorCode, string Message) : Failure
{
    public static Panic Create(string message) => new(message);
}
