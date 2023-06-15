///
/// Author: Jasper Baijens
///

namespace Avans.Demo.Domain.Exceptions
{
    /// <inheritdoc />
    public class InvalidGameException : Exception
    {
        /// <inheritdoc />
        public InvalidGameException(string? message) : base(message)
        {
        }
    }
}
