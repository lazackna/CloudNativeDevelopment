using MediatR;

namespace Avans.Demo.Logic.Books.Commands
{
    /// <summary>
    /// Request for deleting a book.
    /// </summary>
    public class DeleteBookRequest : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteBookRequest"/> class.
        /// </summary>
        public DeleteBookRequest(string isbn)
            => Isbn = isbn;

        public string Isbn { get; }
    }
}
