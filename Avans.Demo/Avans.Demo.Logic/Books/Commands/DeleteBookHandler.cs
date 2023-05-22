using Avans.Demo.DataAccess;
using Avans.Demo.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Demo.Logic.Books.Commands
{
    /// <summary>
    /// Implementation for <see cref="DeleteBookRequest"/> message.
    /// Will throw when the book cannot be found.
    /// Otherwise, it will delete the book from the database.
    /// </summary>
    public class DeleteBookHandler : IRequestHandler<DeleteBookRequest, Unit>
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteBookHandler"/> class.
        /// </summary>
        public DeleteBookHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
        {
            var dbItem = await _context.Books.SingleOrDefaultAsync(b => b.ISBN == request.Isbn, cancellationToken: cancellationToken);
            if (dbItem == null)
            {
                throw new BookNotFoundException();
            }
            _context.Books.Remove(dbItem);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
