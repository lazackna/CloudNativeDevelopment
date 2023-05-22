using Microsoft.Extensions.DependencyInjection;

namespace Avans.Demo.DataAccess.InMemory
{
    /// <summary>
    /// See <see cref="SetupDataAccessInMemory(IServiceCollection)"/>
    /// </summary>
    public static class Setup
    {
        /// <summary>
        /// Registers the InMemory implementation for the <see cref="DataContext"/>
        /// This use case is not persistent and is best used for testing.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection SetupDataAccessInMemory(this IServiceCollection services)
        {
            return services
                .AddDbContext<DataContext, InMemoryDataContext>();
        }
    }
}
