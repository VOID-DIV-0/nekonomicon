namespace Nekonomicon.Core.Models;

public record Result<TSuccess, TFailure> where TFailure : Failure
{
  public TSuccess? Success { get; }
  public TFailure? Failure { get; }

  public bool IsSuccess => Success is not null && Failure is null;
  public bool IsFailure => !IsSuccess;

  public Result(TSuccess success) { Success = success; }
  public Result(TFailure failure) { Failure = failure; }

  public static implicit operator Result<TSuccess, TFailure>(TSuccess success) => new(success);
  public static implicit operator Result<TSuccess, TFailure>(TFailure failure) => new(failure);

  public static Result<TSuccess, TFailure> Succeed(TSuccess value) => new(value);
  public static Result<TSuccess, TFailure> Fail(TFailure value) => new(value);

  public void Deconstruct(out TSuccess? success, out TFailure? failure)
  {
      success = Success;
      failure = Failure;
  }

  public TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, Func<TFailure, TResult> onFailure)
  {
      if (IsSuccess)
      {
        return onSuccess(Success!);
      }

      return onFailure(Failure!);
  }
}

public record Failure
{
  public required string Message { get; init; }
}
