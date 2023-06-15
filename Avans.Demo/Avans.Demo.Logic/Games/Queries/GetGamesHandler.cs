using Avans.Demo.DataAccess;
using Avans.Demo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
///
/// Author: Jasper Baijens
///

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Demo.Logic.Games.Queries
{
	public class GetGamesHandler : IRequestHandler<GetGamesRequest, List<ApiGame>>
	{
		private readonly DataContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="GetGamesHandler"/> class.
		/// </summary>
		public GetGamesHandler(DataContext context)
		{
			_context = context;
		}

		public Task<List<ApiGame>> Handle(GetGamesRequest request, CancellationToken cancellationToken)
		{
			return _context.Games
				.Select(b => new ApiGame(
					b.Title,
					b.ReleaseDate,
					b.Team,
					b.Rating,
					b.Genres
					))
				.ToListAsync(cancellationToken);
		}
	}
}
