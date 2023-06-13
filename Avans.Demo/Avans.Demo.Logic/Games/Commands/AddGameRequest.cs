using Avans.Demo.Domain;
using MediatR;

namespace Avans.Demo.Logic.Games.Commands
{
	/// <summary>
	/// Request for adding or updating a book.
	/// </summary>
	public class AddGameRequest : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddGameRequest"/> class.
        /// </summary>
        public AddGameRequest(ApiGame game) 
            => Game = game;

        public ApiGame Game { get; }
    }
}
