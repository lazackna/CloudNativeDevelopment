///
/// Author: Jasper Baijens
///

using Avans.Demo.DataAccess.Entities;
using Avans.Demo.Domain;
using Avans.Demo.Logic.Games.Commands;
using Avans.Demo.Logic.Games.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Avans.Demo.Web.Api.Controllers
{
    
    /// <inheritdoc />
    [Route("v1.0/games")]
    public class GamesController : Controller
    {
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all currently registered games.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of ApiGame</returns>
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ApiGame>))]
        public async Task<IActionResult> GetAllBooksAsync(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetGamesRequest(), cancellationToken));
        }

        /// <summary>
        /// Adds or updates a game, based on <see cref="ApiGame.Title"/>
        /// </summary>
        /// <param name="game"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddBookAsync([FromBody] ApiGame game, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _mediator.Send(new AddGameRequest(game), cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a game on <see cref="ApiGame.Title"/>, if it exists.
        /// Will return a 400 if the game is not found.
        /// One could argue that it should be a 404, but that assumes to much
        /// and this is just a simple demo.
        /// </summary>
        /// <param name="title"><see cref="ApiGame.Title"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{title}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] string title, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteGameRequest(title), cancellationToken));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
