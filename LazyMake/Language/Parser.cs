namespace LazyMake.Language
{
    internal class Parser : IParser
    {
        public IParsedStep[] Parse(IEnumerable<Token> tokens)
        {
            var enumerator = tokens.GetEnumerator();
            var steps = new List<IParsedStep>();
            while (enumerator.MoveNext())
            {
                switch (enumerator.Current.Type)
                {
                    case TokenType.Whitespace:
                        continue;
                    case TokenType.String:
                    case TokenType.QuotedString:
                        steps.Add(ParseStringStep(enumerator));
                        break;
                    case TokenType.DoubleDash:
                        steps.Add(ParseSetVariableStep(enumerator));
                        break;
                    default:
                        throw new SyntaxException($"Unexpcted token type {enumerator.Current.Type}.");
                }
            }

            return [.. steps];
        }

        private ParsedNamedStep ParseStringStep(IEnumerator<Token> enumerator)
        {
            var token = enumerator.Current;

            if (string.IsNullOrWhiteSpace(token.Value))
            {
                throw new InternalErrorException($"Value is expectedly empty for token type {token.Type}.");
            }

            GetWhitespaceUnlessEnd(enumerator);

            return new ParsedNamedStep
            {
                Name = token.Value,
            };
        }

        private ParsedSetVariableStep ParseSetVariableStep(IEnumerator<Token> enumerator)
        {
            if (!(enumerator.Current.Type == TokenType.DoubleDash))
            {
                throw new InternalErrorException("Cannot be called unless the current token is a double dash.");
            }

            string name = GetString(enumerator);

            if (!enumerator.MoveNext())
            {
                throw new SyntaxException("Unexpected end of line.");
            }

            if (enumerator.Current.Type != TokenType.Equals)
            {
                throw new SyntaxException($"Unexpected token. Expected equals but foudn {enumerator.Current.Type}.");
            }

            var value = GetString(enumerator);

            GetWhitespaceUnlessEnd(enumerator);

            return new ParsedSetVariableStep
            {
                Name = name,
                Value = value,
            };
        }

        private string GetString(IEnumerator<Token> enumerator)
        {
            if (!enumerator.MoveNext())
            {
                throw new SyntaxException("Unexpected end of line.");
            }

            var token = enumerator.Current;

            if ((token.Type != TokenType.String)
                && (token.Type != TokenType.QuotedString))
            {
                throw new SyntaxException($"Unexpected token type. Expected string or quoted string but found {token.Type}.");
            }

            if (string.IsNullOrWhiteSpace(token.Value))
            {
                throw new InternalErrorException($"Value is expectedly empty for token type {token.Type}.");
            }

            return token.Value;
        }

        private void GetWhitespaceUnlessEnd(IEnumerator<Token> enumerator)
        {
            if (enumerator.MoveNext())
            {
                if (enumerator.Current.Type != TokenType.Whitespace)
                {
                    throw new SyntaxException($"Expected end of line or whitespace, but found {enumerator.Current.Type}.");
                }
            }
        }
    }
}
