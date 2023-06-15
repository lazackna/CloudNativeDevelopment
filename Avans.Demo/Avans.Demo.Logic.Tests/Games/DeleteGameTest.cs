///
/// Author: Jasper Baijens
///

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
	public class DeleteGameTest
	{
		private readonly IMediator _mediator;
		private readonly DataContext _dbContext;

		public DeleteGameTest()
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
				new Game("Elden ring", "02-02-2008", new string[]{"team 1", "team 2"}, 5, new string[]{"action"})
			});

			_dbContext.SaveChanges();
		}

		[Fact]
		public async Task CannotDeleteNonExistingGame()
		{
			//Arrange
			var title = "mario galaxy";
			var message = new DeleteGameRequest(title);

			//Act && Assert
			await Assert.ThrowsAsync<GameNotFoundException>(
				() => _mediator.Send(message));
		}


		[Fact]
		public async Task HappyFlow()
		{
			//Arrange
			var title = "Elden ring";
			var message = new DeleteGameRequest(title);

			//Act
			await _mediator.Send(message);

			//Assert
			var books = await _mediator.Send(new GetGamesRequest());
			Assert.Empty(books);
		}
	}
}
