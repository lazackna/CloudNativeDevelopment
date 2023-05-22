using Microsoft.EntityFrameworkCore;

namespace Avans.Demo.DataAccess.InMemory
{
    /// <summary>
    /// Implementation of the base <see cref="DataContext"/>
    /// This version implements the context with an InMemory provider.
    /// </summary>
    public class InMemoryDataContext : DataContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            base.OnConfiguring(optionsBuilder);
        }
    }
}
