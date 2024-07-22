namespace LazyMake
{
    /// <summary>
    /// Exception representing an unexpected internal error in the application.
    /// </summary>
    public class InternalErrorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalErrorException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public InternalErrorException(string? message) : base(message)
        {
        }
    }
}
