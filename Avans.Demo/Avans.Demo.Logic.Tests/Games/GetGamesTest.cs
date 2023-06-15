using Avans.Demo.DataAccess.Entities;
using Avans.Demo.DataAccess;
using Avans.Demo.DataAccess;
using Avans.Demo.DataAccess.Entities;
using Avans.Demo.DataAccess.InMemory;
using Avans.Demo.Domain;
using Avans.Demo.Domain.Exceptions;
using Avans.Demo.Logic.Books.Commands;
using Avans.Demo.Logic.Games.Commands;
using Avans.Demo.Logic.Games.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Avans.Demo.Logic.Tests.Games
{
	public class GetGamesTest
	{
		private readonly IMediator _mediator;
		private readonly DataContext _dbContext;

		public GetGamesTest()
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
			_dbContext.Games.AddRange(new[] {
				new Game("Elden ring", "02-02-2008", new string[]{"team 1", "team 2"}, 5, new string[]{"action"}),
				new Game("Omori", "02-10-2016", new string[]{"team 1", "team 2"}, 4.7, new string[]{"horror"}),
				new Game("Mario galaxy", "08-02-2008", new string[]{"team 1", "team 2"}, 4.2, new string[]{"Adventure"})
			});

			_dbContext.SaveChanges();
		}

		[Fact]
		public async Task ShouldReturnAllDatabaseRowsAsync()
		{
			//Arrange
			var message = new GetGamesRequest();

			//Act
			var result = await _mediator.Send(message);

			//Assert
			Assert.Equal(3, result.Count);
		}

		[Fact]
		public async Task RatingIsNotBelowZero()
		{
			//Arrange
			var message = new GetGamesRequest();

			//Act
			var result = await _mediator.Send(message);
			
			bool b = result.Where(g => g.Rating < 0f).Count() == 0;

			//Assert
			Assert.True(b);
		}
	}
}
