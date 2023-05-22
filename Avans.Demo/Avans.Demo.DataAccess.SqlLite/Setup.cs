using Microsoft.Extensions.DependencyInjection;

namespace Avans.Demo.DataAccess.SqlLite
{
    /// <summary>
    /// See <see cref="SetupDataAccessSqlLite(IServiceCollection)"/>
    /// </summary>
    public static class Setup
    {
        /// <summary>
        /// Registers the SQLite implementation for the <see cref="DataContext"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection SetupDataAccessSqlLite(this IServiceCollection services)
        {
            return services
                .AddDbContext<DataContext, SqlLiteDataContext>();
        }
    }
}