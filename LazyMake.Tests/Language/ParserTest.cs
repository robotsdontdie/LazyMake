using FluentAssertions;
using LazyMake.Language;

namespace LazyMake.Tests.Language
{
    [TestClass]
    public class ParserTest
    {
        private readonly Parser parser = new Parser();

        [TestMethod]
        public void Parse_EmptyTokens_ReturnsEmpty()
        {
            parser.Parse([]).Should().BeEmpty();
        }

        [TestMethod]
        public void Parse_Whitespace_ReturnsEmpty()
        {
            var tokens = new Token[]
            {
                Token.GetWhitespace(),
            };

            parser.Parse(tokens).Should().BeEmpty();
        }

        [TestMethod]
        public void Parse_String_ReturnsNamed()
        {
            var tokens = new Token[]
            {
                Token.GetString("123"),
            };

            parser.Parse(tokens).Should().ContainSingle()
                .Which.Should().BeOfType<ParsedNamedStep>()
                .Subject.Name.Should().Be("123");
        }

        [TestMethod]
        public void Parse_QuotedString_ReturnsNamed()
        {
            var tokens = new Token[]
            {
                Token.GetQuotedString("123"),
            };

            parser.Parse(tokens).Should().ContainSingle()
                .Which.Should().BeOfType<ParsedNamedStep>()
                .Subject.Name.Should().Be("123");
        }

        [TestMethod]
        public void Parse_Equals_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetEquals(),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_Whitespace_Ignored()
        {
            var tokens = new Token[]
            {
                Token.GetWhitespace(),
                Token.GetQuotedString("123"),
                Token.GetWhitespace(),
            };

            parser.Parse(tokens).Should().ContainSingle()
                .Which.Should().BeOfType<ParsedNamedStep>()
                .Subject.Name.Should().Be("123");
        }

        [TestMethod]
        public void Parse_WhitespaceWithEquals_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetWhitespace(),
                Token.GetEquals(),
                Token.GetWhitespace(),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_StringEquals_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetString("123"),
                Token.GetEquals(),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_QuotedStringEquals_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetQuotedString("123"),
                Token.GetEquals(),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_StringDoubleDash_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetString("123"),
                Token.GetDoubleDash(),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_QuotedStringDoubleDash_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetQuotedString("123"),
                Token.GetDoubleDash(),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_StringString_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetString("123"),
                Token.GetString("123"),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_StringQuotedString_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetString("123"),
                Token.GetQuotedString("123"),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_DoubleDashEquals_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetDoubleDash(),
                Token.GetEquals(),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_DoubleDashStringDoubleDash_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetDoubleDash(),
                Token.GetString("123"),
                Token.GetDoubleDash(),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_DoubleDashStringEqualsDoubleDash_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetDoubleDash(),
                Token.GetString("123"),
                Token.GetEquals(),
                Token.GetDoubleDash(),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_DoubleDashStringEqualsEquals_Throws()
        {
            var tokens = new Token[]
            {
                Token.GetDoubleDash(),
                Token.GetString("123"),
                Token.GetEquals(),
                Token.GetDoubleDash(),
            };

            parser.Invoking(p => p.Parse(tokens)).Should().Throw<SyntaxException>();
        }

        [TestMethod]
        public void Parse_StringSpaceString_ReturnsTwoSteps()
        {
            var tokens = new Token[]
            {
                Token.GetString("123"),
                Token.GetWhitespace(),
                Token.GetString("123"),
            };

            parser.Parse(tokens).Should().HaveCount(2).And.AllBeOfType<ParsedNamedStep>();
        }

        [TestMethod]
        public void Parse_StringSpaceAssignment_ReturnsTwoSteps()
        {
            var tokens = new Token[]
            {
                Token.GetString("123"),
                Token.GetWhitespace(),
                Token.GetDoubleDash(),
                Token.GetString("name"),
                Token.GetEquals(),
                Token.GetString("value"),
            };

            var steps = parser.Parse(tokens);
            steps.Should().HaveCount(2);
            steps[0].Should().BeOfType<ParsedNamedStep>();
            steps[1].Should().BeOfType<ParsedSetVariableStep>();
        }

        [TestMethod]
        public void Parse_StringSpaceSpaceAssignment_ReturnsTwoSteps()
        {
            var tokens = new Token[]
            {
                Token.GetString("123"),
                Token.GetWhitespace(),
                Token.GetWhitespace(),
                Token.GetDoubleDash(),
                Token.GetString("name"),
                Token.GetEquals(),
                Token.GetString("value"),
            };

            var steps = parser.Parse(tokens);
            steps.Should().HaveCount(2);
            steps[0].Should().BeOfType<ParsedNamedStep>();
            steps[1].Should().BeOfType<ParsedSetVariableStep>();
        }

        [TestMethod]
        public void Parse_AssignmentSpaceString_ReturnsTwoSteps()
        {
            var tokens = new Token[]
            {
                Token.GetDoubleDash(),
                Token.GetString("name"),
                Token.GetEquals(),
                Token.GetString("value"),
                Token.GetWhitespace(),
                Token.GetString("123"),
            };

            var steps = parser.Parse(tokens);
            steps.Should().HaveCount(2);
            steps[0].Should().BeOfType<ParsedSetVariableStep>();
            steps[1].Should().BeOfType<ParsedNamedStep>();
        }
    }
}
