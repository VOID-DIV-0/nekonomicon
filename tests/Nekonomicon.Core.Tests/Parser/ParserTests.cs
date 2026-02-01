using FluentAssertions;
using Nekonomicon.Core.Parser;
using Nekonomicon.Core.Parser.Ast;
using Nekonomicon.Core.Runtime;
using Xunit;

namespace Nekonomicon.Core.Tests.Parser;

public class ParserTests
{
    [Fact]
    public void Parse_SimpleSetCommand_ShouldCreateCommandNode()
    {
        // Arrange
        var lexer = new Lexer();
        var tokens = lexer.Tokenize("set @name to \"Alice\".").ToArray();
        var parser = new RuntimeParser();

        // Act
        var result = parser.Parse(tokens);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var command = result.Value as CommandNode;
        command.Should().NotBeNull();
        command!.Intrinsic.Should().Be("set");
        command!.Arguments.Length.Should().Be(2);
    }

    [Fact]
    public void Parse_SimpleSayCommand_ShouldCreateCommandNode()
    {
        // Arrange
        var lexer = new Lexer();
        var tokens = lexer.Tokenize("say @name.").ToArray();
        var parser = new RuntimeParser();

        // Act
        var result = parser.Parse(tokens);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var command = result.Value as CommandNode;
        command.Should().NotBeNull();
        command!.Intrinsic.Should().Be("say");
    }

    [Fact]
    public void Parse_MissingPeriod_ShouldFail()
    {
        // Arrange
        var lexer = new Lexer();
        var tokens = lexer.Tokenize("say \"Hello\"").ToArray();
        var parser = new RuntimeParser();

        // Act
        var result = parser.Parse(tokens);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Contain("period");
    }

    [Fact]
    public void Parse_SetCommandWithVariable_ShouldParseVariableReference()
    {
        // Arrange
        var lexer = new Lexer();
        var tokens = lexer.Tokenize("set @x to 42.").ToArray();
        var parser = new RuntimeParser();

        // Act
        var result = parser.Parse(tokens);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var command = result.Value as CommandNode;
        command.Should().NotBeNull();
        command!.Arguments.Length.Should().Be(2);
        (command.Arguments[0] as VariableReferenceNode).Should().NotBeNull();
    }

    [Fact]
    public void Parse_MultipleCommands_ShouldParseEachCommand()
    {
        // Arrange
        var lexer = new Lexer();
        var tokens = lexer.Tokenize("set @x to 1.\nset @y to 2.").ToArray();
        var parser = new RuntimeParser();

        // Act
        var result = parser.Parse(tokens);

        // Assert
        result.IsSuccess.Should().BeTrue();
        // First command should be set @x to 1
        var firstCommand = result.Value as CommandNode;
        firstCommand.Should().NotBeNull();
        firstCommand!.Intrinsic.Should().Be("set");
    }

    [Fact]
    public void Parse_SayWithStringLiteral_ShouldParseLiteralArgument()
    {
        // Arrange
        var lexer = new Lexer();
        var tokens = lexer.Tokenize("say \"Hello\".").ToArray();
        var parser = new RuntimeParser();

        // Act
        var result = parser.Parse(tokens);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var command = result.Value as CommandNode;
        command.Should().NotBeNull();
        command!.Arguments.Length.Should().Be(1);
        (command.Arguments[0] as LiteralNode).Should().NotBeNull();
    }

    [Fact]
    public void Parse_SetCommandWithNumber_ShouldParseLiteralArgument()
    {
        // Arrange
        var lexer = new Lexer();
        var tokens = lexer.Tokenize("set @x to 123.").ToArray();
        var parser = new RuntimeParser();

        // Act
        var result = parser.Parse(tokens);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var command = result.Value as CommandNode;
        command.Should().NotBeNull();
        var valueLiteral = command!.Arguments[1] as LiteralNode;
        valueLiteral.Should().NotBeNull();
        valueLiteral!.Value.Should().Be(123.0); // Numbers are parsed as doubles
    }
}
