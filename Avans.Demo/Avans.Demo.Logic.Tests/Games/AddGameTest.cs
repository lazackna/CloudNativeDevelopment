///
/// Author: Jasper Baijens
///


using Avans.Demo.DataAccess.InMemory;
using Avans.Demo.Domain;
using Avans.Demo.Domain.Exceptions;
using Avans.Demo.Logic.Games.Commands;
using Avans.Demo.Logic.Games.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Avans.Demo.Logic.Tests.Games
{
	public class AddGameTest
	{
		private readonly IMediator _mediator;

		public AddGameTest()
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
			ApiGame book = null;
			var message = new AddGameRequest(book);

			//Act && Assert
			await Assert.ThrowsAsync<GameCannotBeNullException>(
				() => _mediator.Send(message));
		}

		[Fact]
		public async Task CannotAddGameWithoutTitle()
		{
			//Arrange
			var book = new ApiGame(null, null, "", 0, null); 
			var message = new AddGameRequest(book);

			//Act && Assert
			await Assert.ThrowsAsync<InvalidGameException>(
				() => _mediator.Send(message));
		}

		[Fact]
		public async Task HappyFlow()
		{
			//Arrange
			var book = new ApiGame("Test game", "24-02-2018", "test team,test team2", 5, "action,sandbox");
			var message = new AddGameRequest(book);

			//Act
			await _mediator.Send(message);

			//Assert
			var books = await _mediator.Send(new GetGamesRequest());
			Assert.Single(books);
		}
	}
}
