using Avans.Demo.DataAccess.Configuration;
using Avans.Demo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Avans.Demo.DataAccess
{
    /// <summary>
    /// Base Entity Framework model for use within Logic.
    /// Note that this is an abstract and will need an actual
    /// provider to implement this interface.
    /// This can be InMemory with Avans.Demo.DataAccess.InMemory.InMemoryDataContext
    /// or with SQLite with Avans.Demo.DataAccess.SqlLite.SqlLiteDataContext
    /// 
    /// This abstract will setup the the model configuration.
    /// </summary>
    public abstract class DataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }
    }
}
