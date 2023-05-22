using Avans.Demo.DataAccess;
using Avans.Demo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Avans.Demo.Logic.Books.Queries
{
    /// <summary>
    /// Implementation for <see cref="GetBooksRequest"/> message.
    /// Will load from the database the books and maps them
    /// to the <see cref="ApiBook"/> model.
    /// </summary>
    public class GetBooksHandler : IRequestHandler<GetBooksRequest, List<ApiBook>>
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetBooksHandler"/> class.
        /// </summary>
        public GetBooksHandler(DataContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public Task<List<ApiBook>> Handle(GetBooksRequest request, CancellationToken cancellationToken)
        {
            return _context.Books
                .Select(b => new ApiBook
                {
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Pages = b.Pages,
                    Title = b.Title,
                    Website = b.Website
                })
                .ToListAsync(cancellationToken);
        }
    }
}