namespace Avans.Demo.Domain.Exceptions
{
    /// <inheritdoc />
    public class InvalidBookException : Exception
    {
        /// <inheritdoc />
        public InvalidBookException(string? message) : base(message)
        {
        }
    }
}
