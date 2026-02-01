using FluentAssertions;
using Nekonomicon.Core.Parser.Ast;
using Xunit;

namespace Nekonomicon.Core.Tests.Parser.Ast;

public class AstNodeTests
{
    [Fact]
    public void AstNode_WithValidLineAndColumn_ShouldConstruct()
    {
        // Arrange & Act
        var node = new CommandNode("set", Array.Empty<AstNode>(), 1, 1);

        // Assert
        node.Line.Should().Be(1);
        node.Column.Should().Be(1);
    }

    [Fact]
    public void AstNode_WithZeroLine_ShouldThrow()
    {
        // Arrange, Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new CommandNode("set", Array.Empty<AstNode>(), 0, 1));
        ex.Message.Should().Contain("Line must be greater than 0");
    }

    [Fact]
    public void CommandNode_ShouldStoreIntrinsicAndArguments()
    {
        // Arrange
        var arg = new LiteralNode(LiteralType.String, "hello", 1, 5);

        // Act
        var node = new CommandNode("say", new[] { arg }, 1, 1);

        // Assert
        node.Intrinsic.Should().Be("say");
        node.Arguments.Should().HaveCount(1);
        node.Arguments[0].Should().Be(arg);
    }

    [Fact]
    public void VariableReferenceNode_ShouldStoreNameAndModifier()
    {
        // Act
        var node = new VariableReferenceNode("name", VariableModifier.Mutable, 1, 1);

        // Assert
        node.Name.Should().Be("name");
        node.Modifier.Should().Be(VariableModifier.Mutable);
    }

    [Fact]
    public void VariableReferenceNode_WithConstantModifier_ShouldStore()
    {
        // Act
        var node = new VariableReferenceNode("maxSize", VariableModifier.Constant, 1, 1);

        // Assert
        node.Modifier.Should().Be(VariableModifier.Constant);
    }

    [Fact]
    public void VariableReferenceNode_WithNullableModifier_ShouldStore()
    {
        // Act
        var node = new VariableReferenceNode("optional", VariableModifier.Nullable, 1, 1);

        // Assert
        node.Modifier.Should().Be(VariableModifier.Nullable);
    }

    [Fact]
    public void LiteralNode_WithStringValue_ShouldStore()
    {
        // Act
        var node = new LiteralNode(LiteralType.String, "hello", 1, 1);

        // Assert
        node.Type.Should().Be(LiteralType.String);
        node.Value.Should().Be("hello");
    }

    [Fact]
    public void LiteralNode_WithNumberValue_ShouldStore()
    {
        // Act
        var node = new LiteralNode(LiteralType.Number, 42, 1, 1);

        // Assert
        node.Type.Should().Be(LiteralType.Number);
        node.Value.Should().Be(42);
    }

    [Fact]
    public void LiteralNode_WithBooleanValue_ShouldStore()
    {
        // Act
        var node = new LiteralNode(LiteralType.Boolean, true, 1, 1);

        // Assert
        node.Type.Should().Be(LiteralType.Boolean);
        node.Value.Should().Be(true);
    }
}
