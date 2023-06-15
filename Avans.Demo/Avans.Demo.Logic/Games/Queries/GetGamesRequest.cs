///
/// Author: Jasper Baijens
///

using Avans.Demo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Demo.Logic.Games.Queries
{
	public class GetGamesRequest : IRequest<List<ApiGame>>
	{
	}
}
