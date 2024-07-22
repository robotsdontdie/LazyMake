namespace LazyMake.Language
{
    /// <summary>
    /// The type of a particular <see cref="Token"/>s.
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// A token representing a sequence of whitespace characters.
        /// </summary>
        Whitespace,

        /// <summary>
        /// A token representing two hyphen, ie "--".
        /// </summary>
        DoubleDash,

        /// <summary>
        /// A token representing an equals sign, ie "=".
        /// </summary>
        Equals,

        /// <summary>
        /// A token representing a string of letters or digits, eg "abc".
        /// </summary>
        String,

        /// <summary>
        /// A token representing a string of letters or digits that was quoted in the command line. The token iteslf will not include the quotes.
        /// </summary>
        QuotedString,
    }
}
