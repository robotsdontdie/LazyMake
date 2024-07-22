namespace LazyMake.Language
{
    /// <summary>
    /// An exception while trying to process the syntax of a command line.
    /// </summary>
    public class SyntaxException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyntaxException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public SyntaxException(string? message) : base(message)
        {
        }
    }
}
