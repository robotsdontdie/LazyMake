namespace LazyMake.Language
{
    /// <summary>
    /// A lexer to tokenize a command line into tokens.
    /// </summary>
    public interface ILexer
    {
        /// <summary>
        /// Tokenizes a line into tokens.
        /// </summary>
        /// <param name="line">The line to tokenize.</param>
        /// <returns>The tokens.</returns>
        Token[] Tokenize(string line);
    }
}
