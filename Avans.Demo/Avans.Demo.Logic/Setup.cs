using Microsoft.Extensions.DependencyInjection;

namespace Avans.Demo.Logic
{
    /// <summary>
    /// See <see cref="SetupApplication(IServiceCollection)"/>
    /// </summary>
    public static class Setup
    {
        /// <summary>
        /// Registers Mediator for all requests in the Assembly 
        /// with <see cref="ILogicAssemblyMarker"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection SetupApplication(this IServiceCollection services)
        {
            return services
                .AddMediatR(config => config.RegisterServicesFromAssemblyContaining<ILogicAssemblyMarker>());
        }
    }
}