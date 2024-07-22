using FluentAssertions;
using LazyMake.Language;

namespace LazyMake.Tests.Language
{
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void EqualsOperator_WithTwoIdenticalTokens_ReturnsTrue()
        {
            var token1 = Token.GetString("123");
            var token2 = Token.GetString("123");

            (token1 == token2).Should().BeTrue();
        }

        [TestMethod]
        public void EqualsOverride_WithTwoIdenticalTokens_ReturnsTrue()
        {
            var token1 = Token.GetString("123");
            var token2 = Token.GetString("123");

            token1.Equals(token2).Should().BeTrue();
        }

        [TestMethod]
        public void StaticEquals_WithTwoIdenticalTokens_ReturnsTrue()
        {
            var token1 = Token.GetString("123");
            var token2 = Token.GetString("123");

            object.Equals(token1, token2).Should().BeTrue();
        }

        [TestMethod]
        public void GetString_WithAnything_ReturnsTokenWithThatString()
        {
            var token = Token.GetString("123");

            token.ShouldBeString("123");
        }

        [TestMethod]
        public void GetQuotedString_WithAnything_ReturnsTokenWithThatString()
        {
            var token = Token.GetQuotedString("123");

            token.ShouldBeQuotedString("123");
        }

        [TestMethod]
        public void GetEquals_NoParams_ReturnsEqualsToken()
        {
            var token = Token.GetEquals();

            token.ShouldBeEquals();
        }

        [TestMethod]
        public void GetWhitespace_NoParams_ReturnsWhitespaceToken()
        {
            var token = Token.GetWhitespace();

            token.ShouldBeWhitespace();
        }

        [TestMethod]
        public void GetDoubleDash_NoParams_ReturnsDoubleDashToken()
        {
            var token = Token.GetDoubleDash();

            token.ShouldBeDoubleDash();
        }
    }
}
