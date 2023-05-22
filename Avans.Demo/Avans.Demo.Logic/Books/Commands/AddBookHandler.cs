using Avans.Demo.DataAccess;
using Avans.Demo.DataAccess.Entities;
using Avans.Demo.Domain.Exceptions;
using Avans.Demo.Domain.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Avans.Demo.Logic.Books.Commands
{
    /// <summary>
    /// Implementation for <see cref="AddBookRequest"/> message.
    /// Will validate and throw if the book is null or invalid.
    /// Otherwise, it will add or update into the database.
    /// </summary>
    public class AddBookHandler : IRequestHandler<AddBookRequest, Unit>
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddBookHandler"/> class.
        /// </summary>
        public AddBookHandler(DataContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(AddBookRequest request, CancellationToken cancellationToken)
        {
            Validate(request);

            var book = await _context.Books.SingleOrDefaultAsync(b => b.ISBN == request.Book.ISBN, cancellationToken: cancellationToken);
            if (book == null)
            {
                book = new Book
                {
                    ISBN = request.Book.ISBN
                };
                _context.Books.Add(book);
            }

            book.Author = request.Book.Author;
            book.Pages = request.Book.Pages;
            book.Title = request.Book.Title;
            book.Website = request.Book.Website;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private static void Validate(AddBookRequest request)
        {
            if (request.Book == null)
            {
                throw new BookCannotBeNullException();
            }
            var validator = new ApiBookValidator();
            var validationResult = validator.Validate(request.Book);
            if (!validationResult.IsValid)
            {
                throw new InvalidBookException(validationResult.ToString(Environment.NewLine));
            }
        }
    }
}
