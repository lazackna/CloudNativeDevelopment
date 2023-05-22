using Avans.Demo.DataAccess;
using Avans.Demo.DataAccess.Entities;
using Avans.Demo.DataAccess.InMemory;
using Avans.Demo.Logic.Books.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Avans.Demo.Logic.Tests.Books
{
    public class GetBooksTest
    {
        private readonly IMediator _mediator;
        private readonly DataContext _dbContext;

        public GetBooksTest()
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
                    ISBN = "9781484200766",
                    Title = "Pro Git",
                    Author = "Scott Chacon and Ben Straub",
                    Pages = 458,
                    Website = @"https://git-scm.com/book/en/v2"
                },
                new Book
                {
                    ISBN = "9781484242216",
                    Title = "Rethinking Productivity in Software Engineering",
                    Author = "Caitlin Sadowski, Thomas Zimmermann",
                    Pages = 310,
                    Website = @"https://doi.org/10.1007/978-1-4842-4221-6"
                },
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
        public async Task ShouldReturnAllDatabaseRowsAsync()
        {
            //Arrange
            var message = new GetBooksRequest();

            //Act
            var result = await _mediator.Send(message);

            //Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task ReturnsBooksWithNoWebsite()
        {

            //Arrange
            var message = new GetBooksRequest();

            //Act
            var result = await _mediator.Send(message);

            //Assert
            Assert.Equal(1, result.Count(b => string.IsNullOrWhiteSpace(b.Website)));
        }
    }
}
