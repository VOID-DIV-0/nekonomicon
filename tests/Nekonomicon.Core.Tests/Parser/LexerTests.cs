using FluentAssertions;
using Nekonomicon.Core.Parser;
using Xunit;

namespace Nekonomicon.Core.Tests.Parser;

public class LexerTests
{
    private readonly Lexer _lexer = new();

    [Fact]
    public void Tokenize_WithSimpleKeywords_ShouldIdentifyKeywords()
    {
        // Arrange
        var script = "set say decide invoke if then else end";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        tokens.Should().HaveCountGreaterThan(0);
        tokens[0].Type.Should().Be(TokenType.Keyword);
        tokens[0].Value.Should().Be("set");
    }

    [Fact]
    public void Tokenize_WithVariable_ShouldIdentifyVariableModifiers()
    {
        // Arrange
        var script = "@name @!constant @?nullable";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        tokens.Should().ContainSingle(t => t.Type == TokenType.Identifier && t.Value == "@name");
        tokens.Should().ContainSingle(t => t.Type == TokenType.Identifier && t.Value == "@!constant");
        tokens.Should().ContainSingle(t => t.Type == TokenType.Identifier && t.Value == "@?nullable");
    }

    [Fact]
    public void Tokenize_WithStringLiteral_ShouldIdentifyString()
    {
        // Arrange
        var script = "\"hello world\"";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        var stringToken = tokens.FirstOrDefault(t => t.Type == TokenType.StringLiteral);
        stringToken.Should().NotBeNull();
        stringToken!.Value.Should().Be("hello world");
    }

    [Fact]
    public void Tokenize_WithNumber_ShouldIdentifyNumber()
    {
        // Arrange
        var script = "42 3.14";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        tokens.Should().ContainSingle(t => t.Type == TokenType.NumberLiteral && t.Value == "42");
        tokens.Should().ContainSingle(t => t.Type == TokenType.NumberLiteral && t.Value == "3.14");
    }

    [Fact]
    public void Tokenize_WithPeriod_ShouldIdentifyTerminator()
    {
        // Arrange
        var script = "say \"hello\".";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        tokens.Should().ContainSingle(t => t.Type == TokenType.Period);
    }

    [Fact]
    public void Tokenize_WithSingleLineComment_ShouldSkipComment()
    {
        // Arrange
        var script = "say \"hello\". # This is a comment";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        tokens.Should().NotContain(t => t.Type == TokenType.Comment);
        tokens.Should().ContainSingle(t => t.Type == TokenType.Keyword && t.Value == "say");
    }

    [Fact]
    public void Tokenize_WithMultiLineComment_ShouldSkipComment()
    {
        // Arrange
        var script = "say \"hello\". ### This\nis a\nmulti-line comment ### say \"world\".";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        tokens.Should().NotContain(t => t.Type == TokenType.Comment);
    }

    [Fact]
    public void Tokenize_WithLineEndings_ShouldNormalizeLineNumbers()
    {
        // Arrange - CRLF line endings
        var script = "set @x to 1.\r\nsay @x.";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        var secondSay = tokens.FirstOrDefault(t => t.Type == TokenType.Keyword && t.Value == "say");
        secondSay.Should().NotBeNull();
        secondSay!.Line.Should().Be(2); // Should be on line 2
    }

    [Fact]
    public void Tokenize_WithLFLineEndings_ShouldNormalizeDifferently()
    {
        // Arrange - LF line endings
        var script = "set @x to 1.\nsay @x.";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        var secondSay = tokens.FirstOrDefault(t => t.Type == TokenType.Keyword && t.Value == "say");
        secondSay.Should().NotBeNull();
        secondSay!.Line.Should().Be(2);
    }

    [Fact]
    public void Tokenize_WithEnglishOperators_ShouldIdentifyOperators()
    {
        // Arrange
        var script = "to into with without is greater less than";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        tokens.Should().ContainSingle(t => t.Type == TokenType.To);
        tokens.Should().ContainSingle(t => t.Type == TokenType.Into);
        tokens.Should().ContainSingle(t => t.Type == TokenType.With);
    }

    [Fact]
    public void Tokenize_WithMissingPeriod_ShouldNotIncludePeriod()
    {
        // Arrange
        var script = "say \"hello\"";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        tokens.Should().NotContain(t => t.Type == TokenType.Period);
    }

    [Fact]
    public void Tokenize_ShouldTrackLineAndColumn()
    {
        // Arrange
        var script = "set @x to 5.";

        // Act
        var tokens = _lexer.Tokenize(script);

        // Assert
        tokens.Should().AllSatisfy(t =>
        {
            t.Line.Should().BeGreaterThan(0);
            t.Column.Should().BeGreaterThan(0);
        });
    }
}
