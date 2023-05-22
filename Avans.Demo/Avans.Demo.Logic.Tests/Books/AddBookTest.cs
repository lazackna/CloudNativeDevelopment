using Avans.Demo.DataAccess.InMemory;
using Avans.Demo.Domain;
using Avans.Demo.Domain.Exceptions;
using Avans.Demo.Logic.Books.Commands;
using Avans.Demo.Logic.Books.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Avans.Demo.Logic.Tests.Books
{
    public class AddBookTest
    {
        private readonly IMediator _mediator;

        public AddBookTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .SetupApplication()
                .SetupDataAccessInMemory()
                .BuildServiceProvider();

            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task CannotAddNullAsync()
        {
            //Arrange
            ApiBook book = null;
            var message = new AddBookRequest(book);

            //Act && Assert
            await Assert.ThrowsAsync<BookCannotBeNullException>(
                () => _mediator.Send(message));
        }

        [Fact]
        public async Task CannotAddBookWithoudISBN()
        {
            //Arrange
            var book = new ApiBook
            {
                Author = "author",
                Pages = 10,
                Title = "title"
            };
            var message = new AddBookRequest(book);

            //Act && Assert
            await Assert.ThrowsAsync<InvalidBookException>(
                () => _mediator.Send(message));
        }

        [Fact]
        public async Task HappyFlow()
        {
            //Arrange
            var book = new ApiBook
            {
                ISBN = "isbn",
                Author = "author",
                Pages = 10,
                Title = "title"
            };
            var message = new AddBookRequest(book);

            //Act
            await _mediator.Send(message);

            //Assert
            var books = await _mediator.Send(new GetBooksRequest());
            Assert.Single(books);
        }
    }
}
