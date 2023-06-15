///
/// Author: Jasper Baijens
///

using MediatR;

namespace Avans.Demo.Logic.Games.Commands
{
    /// <summary>
    /// Request for deleting a book.
    /// </summary>
    public class DeleteGameRequest : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteGameRequest"/> class.
        /// </summary>
        public DeleteGameRequest(string title)
            => Title = title;

        public string Title { get; }
    }
}
