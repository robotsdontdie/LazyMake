namespace LazyMake.Language
{
    /// <summary>
    /// A single token in the command line syntax.
    /// </summary>
    public record class Token
    {
        /// <summary>
        /// Creates a token instance for a whitespace token.
        /// </summary>
        /// <returns>The token.</returns>
        public static Token GetWhitespace() => new() { Type = TokenType.Whitespace };

        /// <summary>
        /// Creates a token instance for a dash-dash token.
        /// </summary>
        /// <returns>The token.</returns>
        public static Token GetDoubleDash() => new() { Type = TokenType.DoubleDash };

        /// <summary>
        /// Creates a token instance for a equals token.
        /// </summary>
        /// <returns>The token.</returns>
        public static Token GetEquals() => new() { Type = TokenType.Equals };

        /// <summary>
        /// Creates a token instance for a string token.
        /// </summary>
        /// <param name="str">The token contents.</param>
        /// <returns>The token.</returns>
        public static Token GetString(string str) => new() { Type = TokenType.String, Value = str };

        /// <summary>
        /// Creates a token instance for a quoted string token.
        /// </summary>
        /// <param name="str">The token contents.</param>
        /// <returns>The token.</returns>
        public static Token GetQuotedString(string str) => new() { Type = TokenType.QuotedString, Value = str };

        /// <summary>
        /// Gets the token type.
        /// </summary>
        public TokenType Type { get; init; }

        /// <summary>
        /// Gets the token value.
        /// </summary>
        public string? Value { get; init; } = null;
    }
}
