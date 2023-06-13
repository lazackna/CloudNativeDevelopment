using Avans.Demo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Demo.DataAccess.Configuration
{
	public class GameConfiguration : IEntityTypeConfiguration<Game>
	{
		public void Configure(EntityTypeBuilder<Game> builder)
		{
			builder.HasKey(e => e.Title);
			builder.Property(e => e.ReleaseDate).IsRequired();
			builder.Property(e => e.Team).IsRequired();
			builder.Property(e => e.Rating).IsRequired();
			builder.Property(e => e.Genres).IsRequired();
		}
	}
}
