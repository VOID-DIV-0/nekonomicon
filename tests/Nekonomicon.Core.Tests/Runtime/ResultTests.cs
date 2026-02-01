using FluentAssertions;
using Nekonomicon.Core.Runtime;
using Xunit;

namespace Nekonomicon.Core.Tests.Runtime;

public class ResultTests
{
    [Fact]
    public void Success_ShouldCreateSuccessfulResult()
    {
        // Arrange
        var value = "test";

        // Act
        var result = Result<string>.Success(value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(value);
        result.ErrorMessage.Should().BeEmpty();
        result.Exception.Should().BeNull();
    }

    [Fact]
    public void Success_WithNullValue_ShouldCreateSuccessfulResult()
    {
        // Arrange & Act
        var result = Result<string?>.Success(null);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Failure_ShouldCreateFailedResult()
    {
        // Arrange
        var errorMessage = "Something went wrong";

        // Act
        var result = Result<string>.Failure(errorMessage);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be(errorMessage);
        result.Value.Should().BeNull();
        result.Exception.Should().BeNull();
    }

    [Fact]
    public void Failure_WithException_ShouldIncludeException()
    {
        // Arrange
        var errorMessage = "Error occurred";
        var exception = new InvalidOperationException("Inner error");

        // Act
        var result = Result<string>.Failure(errorMessage, exception);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be(errorMessage);
        result.Exception.Should().Be(exception);
    }

    [Fact]
    public void Match_OnSuccess_ShouldExecuteSuccessCallback()
    {
        // Arrange
        var result = Result<int>.Success(42);

        // Act
        var output = result.Match(
            value => $"Success: {value}",
            error => $"Error: {error}"
        );

        // Assert
        output.Should().Be("Success: 42");
    }

    [Fact]
    public void Match_OnFailure_ShouldExecuteFailureCallback()
    {
        // Arrange
        var result = Result<int>.Failure("Something failed");

        // Act
        var output = result.Match(
            value => $"Success: {value}",
            error => $"Error: {error}"
        );

        // Assert
        output.Should().Be("Error: Something failed");
    }

    [Fact]
    public void Match_WithDifferentReturnType_ShouldWork()
    {
        // Arrange
        var successResult = Result<string>.Success("hello");
        var failureResult = Result<string>.Failure("oops");

        // Act
        var successOutput = successResult.Match(v => v?.Length, _ => 0);
        var failureOutput = failureResult.Match(v => v?.Length, _ => -1);

        // Assert
        successOutput.Should().Be(5);
        failureOutput.Should().Be(-1);
    }
}
