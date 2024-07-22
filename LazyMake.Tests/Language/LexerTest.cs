using FluentAssertions;
using LazyMake.Language;

namespace LazyMake.Tests.Language
{
    [TestClass]
    public class LexerTest
    {
        private readonly Lexer lexer = new();

        [TestMethod]
        public void Tokenize_WithEmptyString_ProducesEmptyArray()
        {
            lexer.Tokenize("").Should().BeEmpty();
        }

        [TestMethod]
        public void Tokenize_WithWhitespace_ProducesSingleWhitespaceToken()
        {
            lexer.Tokenize("  ").Should().ContainSingle()
                .Which.Type.Should().Be(TokenType.Whitespace);
        }

        [TestMethod]
        public void Tokenize_WithSingleString_ProducesSingleStringToken()
        {
            var single = lexer.Tokenize("asdf").Should().ContainSingle().Which;
            single.Type.Should().Be(TokenType.String);
            single.Value.Should().Be("asdf");
        }

        [TestMethod]
        public void Tokenize_WithSingleQuotedString_ProducesSingleStringToken()
        {
            var single = lexer.Tokenize("\"asdf\"").Should().ContainSingle().Which;
            single.Type.Should().Be(TokenType.QuotedString);
            single.Value.Should().Be("asdf");
        }

        [TestMethod]
        public void Tokenize_WithSingleEquals_ProducesSingleEqualsToken()
        {
            var single = lexer.Tokenize("=").Should().ContainSingle().Which;
            single.Type.Should().Be(TokenType.Equals);
            single.Value.Should().BeNull();
        }

        [TestMethod]
        public void Tokenize_WithSingleDoubleDash_ProducesSingleDoubleDashToken()
        {
            var single = lexer.Tokenize("--").Should().ContainSingle().Which;
            single.Type.Should().Be(TokenType.DoubleDash);
            single.Value.Should().BeNull();
        }

        [TestMethod]
        public void Tokenize_WithMultipleStrings_ProducesMultipleStringAndWhitespace()
        {
            var tokens = lexer.Tokenize("123 456 789");
            tokens.Should().HaveCount(5);
            tokens[0].ShouldBeString("123");
            tokens[1].ShouldBeWhitespace();
            tokens[2].ShouldBeString("456");
            tokens[3].ShouldBeWhitespace();
            tokens[4].ShouldBeString("789");
        }

        [TestMethod]
        public void Tokenize_WithStringEqualsString_ProducesThatSequence()
        {
            var tokens = lexer.Tokenize("123=456");
            tokens.Should().HaveCount(3);
            tokens[0].ShouldBeString("123");
            tokens[1].ShouldBeEquals();
            tokens[2].ShouldBeString("456");
        }

        [TestMethod]
        public void Tokenize_WithStringWhitespaceEquals_ProducesThatSequence()
        {
            var tokens = lexer.Tokenize("123 =");
            tokens.Should().HaveCount(3);
            tokens[0].ShouldBeString("123");
            tokens[1].ShouldBeWhitespace();
            tokens[2].ShouldBeEquals();
        }

        [TestMethod]
        public void Tokenize_WithDoubleDashStringEqualsQuoted_ProducesThatSequence()
        {
            var tokens = lexer.Tokenize("--123=\"456\"");
            tokens.Should().HaveCount(4);
            tokens[0].ShouldBeDoubleDash();
            tokens[1].ShouldBeString("123");
            tokens[2].ShouldBeEquals();
            tokens[3].ShouldBeQuotedString("456");
        }

        [TestMethod]
        public void Tokenize_WithLongSequence_ProducesThatSequence()
        {
            var tokens = lexer.Tokenize("abc --123=\"456\" def ===");
            tokens.Should().HaveCount(12);
            tokens[0].ShouldBeString("abc");
            tokens[1].ShouldBeWhitespace();
            tokens[2].ShouldBeDoubleDash();
            tokens[3].ShouldBeString("123");
            tokens[4].ShouldBeEquals();
            tokens[5].ShouldBeQuotedString("456");
            tokens[6].ShouldBeWhitespace();
            tokens[7].ShouldBeString("def");
            tokens[8].ShouldBeWhitespace();
            tokens[9].ShouldBeEquals();
            tokens[10].ShouldBeEquals();
            tokens[11].ShouldBeEquals();
        }

        [TestMethod]
        public void Tokenize_WithStringStartingWithIllegalCharacter_Throws()
        {
            lexer.Invoking(l => l.Tokenize("+")).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Tokenize_WithStringContainingIllegalCharacter_Throws()
        {
            lexer.Invoking(l => l.Tokenize("-")).Should().Throw<SyntaxException>();
        }
    }
}
