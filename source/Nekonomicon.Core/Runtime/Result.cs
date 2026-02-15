namespace Nekonomicon.Core.Runtime;

/// <summary>
/// Represents the result of an operation - either success with a value or failure with an error message.
/// </summary>
public readonly struct Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string ErrorMessage { get; }
    public Exception? Exception { get; }

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
        ErrorMessage = string.Empty;
        Exception = null;
    }

    private Result(string errorMessage, Exception? exception = null)
    {
        IsSuccess = false;
        Value = default;
        ErrorMessage = errorMessage;
        Exception = exception;
    }

    /// <summary>
    /// Creates a successful result with the given value.
    /// </summary>
    public static Result<T> Success(T value) => new(value);

    /// <summary>
    /// Creates a failed result with the given error message.
    /// </summary>
    public static Result<T> Failure(string message, Exception? exception = null) => new(message, exception);

    /// <summary>
    /// Matches on the result - executes onSuccess if successful, onFailure if failed.
    /// </summary>
    public TOut Match<TOut>(Func<T?, TOut> onSuccess, Func<string, TOut> onFailure)
    {
        return IsSuccess ? onSuccess(Value) : onFailure(ErrorMessage);
    }
}
