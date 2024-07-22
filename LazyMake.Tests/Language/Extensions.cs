using FluentAssertions;
using LazyMake.Language;

namespace LazyMake.Tests.Language
{
    internal static class Extensions
    {
        public static void ShouldBe(this Token token, TokenType type, string? value)
        {
            token.Type.Should().Be(type);
            token.Value.Should().Be(value);
        }

        public static void ShouldBeString(this Token token, string value)
            => ShouldBe(token, TokenType.String, value);

        public static void ShouldBeQuotedString(this Token token, string value)
            => ShouldBe(token, TokenType.QuotedString, value);

        public static void ShouldBeWhitespace(this Token token)
            => ShouldBe(token, TokenType.Whitespace, null);

        public static void ShouldBeEquals(this Token token)
            => ShouldBe(token, TokenType.Equals, null);

        public static void ShouldBeDoubleDash(this Token token)
            => ShouldBe(token, TokenType.DoubleDash, null);
    }
}
