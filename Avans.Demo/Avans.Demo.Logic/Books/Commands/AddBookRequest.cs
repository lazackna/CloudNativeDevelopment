using Avans.Demo.Domain;
using MediatR;

namespace Avans.Demo.Logic.Books.Commands
{
    /// <summary>
    /// Request for adding or updating a book.
    /// </summary>
    public class AddBookRequest : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddBookRequest"/> class.
        /// </summary>
        public AddBookRequest(ApiBook book) 
            => Book = book;

        public ApiBook Book { get; }
    }
}
