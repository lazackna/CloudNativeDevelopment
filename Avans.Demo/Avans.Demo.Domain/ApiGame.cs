using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Avans.Demo.Domain
{
	public class ApiGame
	{
		public ApiGame(string title, string releaseDate, string team, double rating, string genres) { 
			Title = title;
			ReleaseDate = releaseDate;
			Team = team?.Split(',');
			Rating = rating;
			Genres = genres?.Split(",");
		}

		[JsonConstructor]
		public ApiGame(string title, string releaseDate, string[] team, double rating, string[] genres)
		{
			Title = title;
			ReleaseDate = releaseDate;
			Team = team;
			Rating = rating;
			Genres = genres;
		}

		//public int Id { get; set; } = -1;
		[Required]
		public string Title { get; set; }
		[Required]
		public string ReleaseDate { get; set; }
		[Required]
		public string[] Team { get; set; }
		[Required]
		public double Rating { get; set; }
		[Required]
		public string[] Genres { get; set; }
	}
}
