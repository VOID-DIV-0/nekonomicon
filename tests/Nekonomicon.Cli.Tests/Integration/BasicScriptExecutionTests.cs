using FluentAssertions;
using Nekonomicon.Core.Runtime;
using Xunit;

namespace Nekonomicon.Cli.Tests.Integration;

public class BasicScriptExecutionTests
{
    [Fact]
    public void ExecuteScript_HelloWorld_ShouldOutputGreeting()
    {
        // Arrange
        var script = "say \"Hello, World\".";
        var engine = new ScriptEngine();

        // Act
        var result = engine.Execute(script);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.Output.Should().Be("Hello, World");
    }

    [Fact]
    public void ExecuteScript_VariableDeclarationAndOutput_ShouldWork()
    {
        // Arrange
        var script = @"set @name to ""Alice"".
say @name.";
        var engine = new ScriptEngine();

        // Act
        var result = engine.Execute(script);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.Output.Should().Be("Alice");
    }

    [Fact]
    public void ExecuteScript_MultipleCommands_ShouldOutputSequentially()
    {
        // Arrange
        var script = @"say ""First"".
say ""Second"".";
        var engine = new ScriptEngine();

        // Act
        var result = engine.Execute(script);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.Output.Should().Be("First\nSecond");
    }

    [Fact]
    public void ExecuteScript_MissingSyntax_ShouldFail()
    {
        // Arrange
        var script = "say \"No period\""; // Missing period terminator
        var engine = new ScriptEngine();

        // Act
        var result = engine.Execute(script);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Contain("period");
    }

    [Fact]
    public void ExecuteScript_UndefinedVariable_ShouldFail()
    {
        // Arrange
        var script = "say @undefined.";
        var engine = new ScriptEngine();

        // Act
        var result = engine.Execute(script);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Contain("not defined");
    }

    [Fact]
    public void ExecuteScript_NumberLiteral_ShouldWork()
    {
        // Arrange
        var script = "say 42.";
        var engine = new ScriptEngine();

        // Act
        var result = engine.Execute(script);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.Output.Should().Be("42");
    }

    [Fact]
    public void ExecuteScript_ConstantVariable_ShouldPreventReassignment()
    {
        // Arrange
        var script = @"set @!constant to 100.
set @!constant to 200.";
        var engine = new ScriptEngine();

        // Act
        var result = engine.Execute(script);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Contain("cannot be reassigned");
    }
}
