using System.Text;

namespace LazyMake.Language
{
    /// <summary>
    /// A cursor for a string, used for tokenization in <see cref="Lexer"/>.
    /// </summary>
    internal class StringCursor
    {
        private readonly string str;
        private int index;

        public StringCursor(string str)
        {
            this.str = str;
            index = 0;
        }

        public bool Finished => index >= str.Length;

        private int RemainingCount => str.Length - index;

        public char Peek()
        {
            if (RemainingCount <= 0)
            {
                throw new IndexOutOfRangeException("No chars remaining.");
            }

            return str[index];
        }

        public bool Peek(char c)
        {
            if (Finished)
            {
                return false;
            }

            return Peek() == c;
        }

        public char ConsumeOne()
        {
            if (RemainingCount <= 0)
            {
                throw new IndexOutOfRangeException("No chars remaining.");
            }

            return str[index++];
        }

        public bool Consume(char c)
        {
            if (Finished)
            {
                return false;
            }

            if (Peek(c))
            {
                index++;
                return true;
            }

            return false;
        }

        public bool Consume(string str)
        {
            if (RemainingCount < str.Length)
            {
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != this.str[index + i])
                {
                    return false;
                }
            }

            index += str.Length;
            return true;
        }

        public string ConsumeAllLetters()
        {
            var letters = new StringBuilder();
            while (!Finished)
            {
                if (!char.IsLetterOrDigit(Peek()))
                {
                    break;
                }

                letters.Append(ConsumeOne());
            }

            return letters.ToString();
        }

        public bool ConsumeAllWhitespace()
        {
            bool found = false;
            while (!Finished)
            {
                if (!char.IsWhiteSpace(Peek()))
                {
                    break;
                }

                ConsumeOne();
                found = true;
            }

            return found;
        }
    }
}
