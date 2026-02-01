using FluentAssertions;
using Nekonomicon.Core.Parser.Ast;
using Nekonomicon.Core.Runtime;
using Xunit;

namespace Nekonomicon.Core.Tests.Runtime;

public class ExecutionContextTests
{
    [Fact]
    public void DeclareVariable_WithValidName_ShouldSucceed()
    {
        // Arrange
        var context = new Nekonomicon.Core.Runtime.ExecutionContext();

        // Act
        var result = context.DeclareVariable("x", VariableModifier.Mutable);

        // Assert
        result.IsSuccess.Should().BeTrue();
        context.VariableExists("x").Should().BeTrue();
    }

    [Fact]
    public void DeclareVariable_WithDuplicate_ShouldFail()
    {
        // Arrange
        var context = new Nekonomicon.Core.Runtime.ExecutionContext();
        context.DeclareVariable("x", VariableModifier.Mutable);

        // Act
        var result = context.DeclareVariable("x", VariableModifier.Mutable);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Contain("already declared");
    }

    [Fact]
    public void DeclareVariable_WithInitialValue_ShouldSetValue()
    {
        // Arrange
        var context = new Nekonomicon.Core.Runtime.ExecutionContext();

        // Act
        context.DeclareVariable("name", VariableModifier.Mutable, "Alice");

        // Assert
        var varResult = context.GetVariable("name");
        varResult.IsSuccess.Should().BeTrue();
        varResult.Value!.Value.Should().Be("Alice");
    }

    [Fact]
    public void GetVariable_WithExistingVariable_ShouldReturn()
    {
        // Arrange
        var context = new Nekonomicon.Core.Runtime.ExecutionContext();
        context.DeclareVariable("x", VariableModifier.Mutable, 42);

        // Act
        var result = context.GetVariable("x");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.Value.Should().Be(42);
    }

    [Fact]
    public void GetVariable_WithUndefinedVariable_ShouldFail()
    {
        // Arrange
        var context = new Nekonomicon.Core.Runtime.ExecutionContext();

        // Act
        var result = context.GetVariable("undefined");

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Contain("not defined");
    }

    [Fact]
    public void SetVariable_WithExistingVariable_ShouldUpdateValue()
    {
        // Arrange
        var context = new Nekonomicon.Core.Runtime.ExecutionContext();
        context.DeclareVariable("x", VariableModifier.Mutable, 1);

        // Act
        var result = context.SetVariable("x", 2);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var varResult = context.GetVariable("x");
        varResult.Value!.Value.Should().Be(2);
    }

    [Fact]
    public void SetVariable_OnConstant_ShouldFail()
    {
        // Arrange
        var context = new Nekonomicon.Core.Runtime.ExecutionContext();
        context.DeclareVariable("max", VariableModifier.Constant, 100);

        // Act
        var result = context.SetVariable("max", 200);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.ToLower().Should().Contain("cannot be reassigned");
    }

    [Fact]
    public void SetVariable_OnUndefined_ShouldFail()
    {
        // Arrange
        var context = new Nekonomicon.Core.Runtime.ExecutionContext();

        // Act
        var result = context.SetVariable("undefined", 42);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Variable_WithNullableModifier_CanBeNull()
    {
        // Arrange
        var context = new Nekonomicon.Core.Runtime.ExecutionContext();
        context.DeclareVariable("optional", VariableModifier.Nullable, null);

        // Act
        var varResult = context.GetVariable("optional");

        // Assert
        varResult.IsSuccess.Should().BeTrue();
        varResult.Value!.Value.Should().BeNull();
    }

    [Fact]
    public void Variable_WithoutNullableModifier_CannotBeNull()
    {
        // Arrange
        var context = new Nekonomicon.Core.Runtime.ExecutionContext();
        context.DeclareVariable("required", VariableModifier.Mutable, "value");

        // Act
        var result = context.SetVariable("required", null);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Contain("cannot be null");
    }
}
