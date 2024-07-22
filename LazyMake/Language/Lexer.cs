using System.Text;

namespace LazyMake.Language
{
    /// <inheritdoc/>
    public class Lexer : ILexer
    {
        private static string Sanitize(string line)
        {
            var sanitized = line.Select(c => c switch
            {
                '\n' => ' ',
                '\r' => ' ',
                _ => c,
            });
            return new string(sanitized.ToArray());
        }

        private static string GetString(StringCursor cursor)
        {
            if (cursor.Finished)
            {
                throw new InternalErrorException("Cursor was finished.");
            }

            string str = cursor.ConsumeAllLetters();
            if (str.Length == 0)
            {
                throw new SyntaxException($"Illegal character {cursor.Peek()}");
            }

            return str;
        }

        private static string GetQuotedString(StringCursor cursor)
        {
            if (!cursor.Consume('"'))
            {
                throw new InternalErrorException("Cursor was not at a quote");
            }

            var str = new StringBuilder();
            while (!cursor.Finished)
            {
                if (cursor.Consume('"'))
                {
                    return str.ToString();
                }

                if (cursor.Consume("\\\""))
                {
                    str.Append('"');
                }
                else
                {
                    str.Append(cursor.ConsumeOne());
                }
            }

            throw new SyntaxException("Quoted string was never closed.");
        }

        /// <inheritdoc/>
        public Token[] Tokenize(string line)
        {
            line = Sanitize(line);
            var cursor = new StringCursor(line);

            var tokens = new List<Token>();

            while (!cursor.Finished)
            {
                if (cursor.ConsumeAllWhitespace())
                {
                    tokens.Add(Token.GetWhitespace());
                }
                else if (cursor.Consume("--"))
                {
                    tokens.Add(Token.GetDoubleDash());
                }
                else if (cursor.Consume('='))
                {
                    tokens.Add(Token.GetEquals());
                }
                else if (cursor.Peek('"'))
                {
                    string str = GetQuotedString(cursor);
                    tokens.Add(Token.GetQuotedString(str));
                }
                else
                {
                    string str = GetString(cursor);
                    tokens.Add(Token.GetString(str));
                }
            }

            return [.. tokens];
        }
    }
}
