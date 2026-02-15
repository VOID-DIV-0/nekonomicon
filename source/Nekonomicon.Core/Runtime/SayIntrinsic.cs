namespace Nekonomicon.Core.Runtime;

/// <summary>
/// Implements the 'say' intrinsic: say <value>.
/// Outputs the value as text.
/// </summary>
public class SayIntrinsic : IIntrinsic
{
    private string? _lastOutput;

    public Result<Unit> Execute(List<object?> arguments, ExecutionContext context)
    {
        if (arguments.Count < 1)
        {
            return Result<Unit>.Failure("say requires at least 1 argument");
        }

        var value = arguments[0];
        var output = FormatOutput(value);
        _lastOutput = output;

        return Result<Unit>.Success(Unit.Default);
    }

    /// <summary>
    /// Returns the last output from this intrinsic instance.
    /// </summary>
    public string? GetLastOutput()
    {
        return _lastOutput;
    }

    private static string FormatOutput(object? value)
    {
        return value switch
        {
            null => "null",
            bool b => b ? "true" : "false",
            double d => d % 1 == 0 ? ((long)d).ToString() : d.ToString(),
            string s => s,
            _ => value.ToString() ?? "null"
        };
    }
}
