using FluentAssertions;
using LazyMake.Language;

namespace LazyMake.Tests.Language
{
    [TestClass]
    public class StringCursorTest
    {
        [TestMethod]
        public void Constructor_OnEmpty_DoesntThrow()
        {
            _ = new StringCursor("");
        }

        [TestMethod]
        public void Constructor_OnContent_DoesntThrow()
        {
            _ = new StringCursor("this is some content");
        }

        [TestMethod]
        public void Finished_OnEmptyString_ReturnsTrue()
        {
            var cursor = new StringCursor("");

            cursor.Finished.Should().BeTrue();
        }

        [TestMethod]
        public void Finished_OnNonEmptyString_ReturnsFalse()
        {
            var cursor = new StringCursor("123");

            cursor.Finished.Should().BeFalse();
        }

        [TestMethod]
        public void Peek_OnEmptyString_Throws()
        {
            var cursor = new StringCursor("");

            cursor.Invoking(c => c.Peek()).Should().Throw<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Peek_OnSingleChar_ReturnsIt()
        {
            var cursor = new StringCursor("1");

            cursor.Peek().Should().Be('1');
        }

        [TestMethod]
        public void Peek_OnMultipleChars_ReturnsFirst()
        {
            var cursor = new StringCursor("123");

            cursor.Peek().Should().Be('1');
        }

        [TestMethod]
        public void Peek_AfterConsumeOneOnLongString_ReturnsSecondChar()
        {
            var cursor = new StringCursor("1234");

            cursor.ConsumeOne();
            cursor.Peek().Should().Be('2');
        }

        [TestMethod]
        public void Peek_WithAnyCharOnEmptyString_ReturnsFalse()
        {
            var cursor = new StringCursor("");

            cursor.Peek('1').Should().BeFalse();
        }

        [TestMethod]
        public void Peek_WithMatchingCharOnSingleCharString_ReturnsTrue()
        {
            var cursor = new StringCursor("1");

            cursor.Peek('1').Should().BeTrue();
        }

        [TestMethod]
        public void Peek_WithDifferentCharOnSingleCharString_ReturnsFalse()
        {
            var cursor = new StringCursor("1");

            cursor.Peek('2').Should().BeFalse();
        }

        [TestMethod]
        public void Peek_AfterConsumeWithMatchingChar_ReturnsTrue()
        {
            var cursor = new StringCursor("123");

            cursor.ConsumeOne();
            cursor.Peek('2').Should().BeTrue();
        }

        [TestMethod]
        public void Peek_AfterConsumeWithNonMatchingChar_ReturnsFalse()
        {
            var cursor = new StringCursor("123");

            cursor.ConsumeOne();
            cursor.Peek('1').Should().BeFalse();
        }

        [TestMethod]
        public void ConsumeOne_OnEmptyString_Throws()
        {
            var cursor = new StringCursor("");

            cursor.Invoking(c => c.ConsumeOne()).Should().Throw<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void ConsumeOne_TwiceOnSingleCharString_Throws()
        {
            var cursor = new StringCursor("1");

            cursor.ConsumeOne();
            cursor.Invoking(c => c.ConsumeOne()).Should().Throw<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void ConsumeOne_OnNonemptyStringThenPeek_ReturnsSecondChar()
        {
            var cursor = new StringCursor("123");

            cursor.ConsumeOne();
            cursor.Peek().Should().Be('2');
        }

        [TestMethod]
        public void Consume_WithCharOnEmptyString_ReturnsFalse()
        {
            var cursor = new StringCursor("");

            cursor.Consume('1').Should().BeFalse();
        }

        [TestMethod]
        public void Consume_WithNonmatchingChar_ReturnsFalseAndDoesntMoveCursor()
        {
            var cursor = new StringCursor("123");

            cursor.Consume('4').Should().BeFalse();
            cursor.Peek().Should().Be('1');
        }

        [TestMethod]
        public void Consume_WithOnceMatchingChar_ReturnsTrueAndMovesCursor()
        {
            var cursor = new StringCursor("123");

            cursor.Consume('1').Should().BeTrue();
            cursor.Peek().Should().Be('2');
        }

        [TestMethod]
        public void Consume_WithMultipleMatchingChar_ReturnsTrueAndMovesCursorOnlyOnce()
        {
            var cursor = new StringCursor("1123");

            cursor.Consume('1').Should().BeTrue();
            cursor.Peek().Should().Be('1');
        }

        [TestMethod]
        public void Consume_WithMultipleMatchingCharAfterFirst_ReturnsTrueAndMovesCursor()
        {
            var cursor = new StringCursor("01123");

            cursor.ConsumeOne();
            cursor.Consume('1').Should().BeTrue();
            cursor.Peek().Should().Be('1');
            cursor.ConsumeOne();
            cursor.Peek().Should().Be('2');
        }

        [TestMethod]
        public void Consume_WithStringOnEmptyString_ReturnsFalse()
        {
            var cursor = new StringCursor("");

            cursor.Consume("123").Should().BeFalse();
        }

        [TestMethod]
        public void Consume_WithMatchingSingleCharString_ReturnsTrueAndMovesCursor()
        {
            var cursor = new StringCursor("123");

            cursor.Consume("1").Should().BeTrue();
            cursor.Peek().Should().Be('2');
        }

        [TestMethod]
        public void Consume_WithMatchingMultiCharString_ReturnsTrueAndMovesCursor()
        {
            var cursor = new StringCursor("123");

            cursor.Consume("12").Should().BeTrue();
            cursor.Peek().Should().Be('3');
        }

        [TestMethod]
        public void Consume_WithNonmatchingString_ReturnsFalseAndDoesntMoveCursor()
        {
            var cursor = new StringCursor("123");

            cursor.Consume("2").Should().BeFalse();
            cursor.Peek().Should().Be('1');
        }

        [TestMethod]
        public void Consume_WithPartiallyMatchingString_ReturnsFalseAndDoesntMoveCursor()
        {
            var cursor = new StringCursor("123");

            cursor.Consume("14").Should().BeFalse();
            cursor.Peek().Should().Be('1');
        }

        [TestMethod]
        public void ConsumeAllLetters_OnEmptyString_ReturnsEmptyString()
        {
            var cursor = new StringCursor("");

            cursor.ConsumeAllLetters().Should().BeEmpty();
        }

        [TestMethod]
        public void ConsumeAllLetters_OnStringOfLetters_ReturnsAllOfThem()
        {
            var cursor = new StringCursor("123");

            cursor.ConsumeAllLetters().Should().Be("123");
        }

        [TestMethod]
        public void ConsumeAllLetters_OnStringStartingWithNonLetter_ReturnsEmpty()
        {
            var cursor = new StringCursor("=123");

            cursor.ConsumeAllLetters().Should().BeEmpty();
        }

        [TestMethod]
        public void ConsumeAllLetters_OnStringOfLettersThenNonLetters_ReturnsJustTheFirstLetters()
        {
            var cursor = new StringCursor("123=456");

            cursor.ConsumeAllLetters().Should().Be("123");
        }

        [TestMethod]
        public void ConsumeAllLetters_OnWhitespace_ReturnsEmpty()
        {
            var cursor = new StringCursor("  123=456");

            cursor.ConsumeAllLetters().Should().BeEmpty();
        }

        [TestMethod]
        public void ConsumeAllWhitespace_OnEmptyString_ReturnsFalse()
        {
            var cursor = new StringCursor("");

            cursor.ConsumeAllWhitespace().Should().BeFalse();
        }

        [TestMethod]
        public void ConsumeAllWhitespace_OnStringStartingWithLetters_ReturnsFalse()
        {
            var cursor = new StringCursor("123");

            cursor.ConsumeAllWhitespace().Should().BeFalse();
        }

        [TestMethod]
        public void ConsumeAllWhitespace_OnStringStartingWithWhitespace_ReturnsTrueAndMovesCursor()
        {
            var cursor = new StringCursor("  123");

            cursor.ConsumeAllWhitespace().Should().BeTrue();
            cursor.Peek().Should().Be('1');
        }
    }
}
