using Avans.Demo.DataAccess;
using Avans.Demo.DataAccess.Entities;
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
    public class DeleteBookTest
    {
        private readonly IMediator _mediator;
        private readonly DataContext _dbContext;

        public DeleteBookTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .SetupApplication()
                .SetupDataAccessInMemory()
                .BuildServiceProvider();


            _mediator = serviceProvider.GetRequiredService<IMediator>();
            _dbContext = serviceProvider.GetRequiredService<DataContext>();

            InitializeData();
        }

        private void InitializeData()
        {
            _dbContext.Books.AddRange(new[] {
                new Book
                {
                    ISBN = "9780135957059",
                    Title = "The Pragmatic Programmer: journey to mastery",
                    Author = "David Thomas and Andrew Hunt",
                    Pages = 321
                }
            });

            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task CannotDeleteNonExistingBook()
        {
            //Arrange
            var isbn = "isbn";
            var message = new DeleteBookRequest(isbn);

            //Act && Assert
            await Assert.ThrowsAsync<BookNotFoundException>(
                () => _mediator.Send(message));
        }


        [Fact]
        public async Task HappyFlow()
        {
            //Arrange
            var isbn = "9780135957059";
            var message = new DeleteBookRequest(isbn);

            //Act
            await _mediator.Send(message);

            //Assert
            var books = await _mediator.Send(new GetBooksRequest());
            Assert.Empty(books);
        }
    }
}
