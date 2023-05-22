using Avans.Demo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Avans.Demo.DataAccess.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.ISBN);
            builder.Property(e => e.Author).IsRequired();
            builder.Property(e => e.Title).IsRequired();
        }
    }
}
