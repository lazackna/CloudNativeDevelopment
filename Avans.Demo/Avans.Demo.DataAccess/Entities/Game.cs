using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Demo.DataAccess.Entities
{
	public class Game
	{
		public Game() { }
		public Game(string title, string releaseDate, string[] team, double rating, string[] genres)
		{
			Title = title;
			ReleaseDate = releaseDate;
			Team = team?.ToString().Replace("[", "").Replace("]", "");
			Rating = rating;
			Genres = genres?.ToString().Replace("[", "").Replace("]", "");
		}

		//public int Id { get; set; } = -1;
		public string Title { get; set; }
		public string ReleaseDate { get; set; }
		public string Team { get; set;  }
		public double Rating { get; set; }
		public string Genres { get; set; }
	}
}
