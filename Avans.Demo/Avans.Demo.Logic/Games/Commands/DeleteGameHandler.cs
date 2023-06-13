using Avans.Demo.DataAccess;
using Avans.Demo.Domain.Exceptions;
using Avans.Demo.Logic.Games.Commands;
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
    /// Implementation for <see cref="DeleteGameRequest"/> message.
    /// Will throw when the book cannot be found.
    /// Otherwise, it will delete the book from the database.
    /// </summary>
    public class DeleteGameHandler : IRequestHandler<DeleteGameRequest, Unit>
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteGameHandler"/> class.
        /// </summary>
        public DeleteGameHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteGameRequest request, CancellationToken cancellationToken)
        {
            var dbItem = await _context.Games.SingleOrDefaultAsync(b => b.Title == request.Title, cancellationToken: cancellationToken);
            if (dbItem == null)
            {
                throw new BookNotFoundException();
            }
            _context.Games.Remove(dbItem);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
