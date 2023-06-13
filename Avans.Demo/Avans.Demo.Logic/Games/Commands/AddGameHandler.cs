using Avans.Demo.DataAccess;
using Avans.Demo.DataAccess.Entities;
using Avans.Demo.Domain.Exceptions;
using Avans.Demo.Domain.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Avans.Demo.Logic.Games.Commands
{
    /// <summary>
    /// Implementation for <see cref="AddGameRequest"/> message.
    /// Will validate and throw if the book is null or invalid.
    /// Otherwise, it will add or update into the database.
    /// </summary>
    public class AddGameHandler : IRequestHandler<AddGameRequest, Unit>
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddGameHandler"/> class.
        /// </summary>
        public AddGameHandler(DataContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(AddGameRequest request, CancellationToken cancellationToken)
        {
            Validate(request);

            var game = await _context.Games.SingleOrDefaultAsync(b => b.Title == request.Game.Title, cancellationToken: cancellationToken);
            if (game == null)
            {
                game = new Game()
                {
                    Title = request.Game.Title
                };
                _context.Games.Add(game);
            }

            game.ReleaseDate = request.Game.ReleaseDate;
            game.Team = request.Game.Team.ToString().Replace("[", "").Replace("]", "");
            game.Rating = request.Game.Rating;
            game.Genres = request.Game.Genres.ToString().Replace("[", "").Replace("]", "");

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private static void Validate(AddGameRequest request)
        {
            if (request.Game == null)
            {
                throw new GameCannotBeNullException();
            }
            var validator = new ApiGameValidator();
            var validationResult = validator.Validate(request.Game);
            if (!validationResult.IsValid)
            {
                throw new InvalidGameException(validationResult.ToString(Environment.NewLine));
            }
        }
    }
}
