using Avans.Demo.DataAccess.Entities;
using Avans.Demo.Domain;
using Avans.Demo.Logic.Books.Commands;
using Avans.Demo.Logic.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Avans.Demo.Web.Api.Controllers
{
    /// <inheritdoc />
    [Route("v1.0/books")]
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all currently registered books.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of ApiBook</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ApiBook>))]
        public async Task<IActionResult> GetAllBooksAsync(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetBooksRequest(), cancellationToken));
        }

        /// <summary>
        /// Adds or updates a book, based on <see cref="ApiBook.ISBN"/>
        /// </summary>
        /// <param name="book"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddBookAsync([FromBody] ApiBook book, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _mediator.Send(new AddBookRequest(book), cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a book on <see cref="ApiBook.ISBN"/>, if it exists.
        /// Will return a 400 if the book is not found.
        /// One could argue that it should be a 404, but that assumes to much
        /// and this is just a simple demo.
        /// </summary>
        /// <param name="isbn"><see cref="ApiBook.ISBN"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{isbn}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] string isbn, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteBookRequest(isbn), cancellationToken));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
