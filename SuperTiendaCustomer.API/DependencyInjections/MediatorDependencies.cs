using MediatR;
using SuperTiendaCustomer.Application.Behaviors;

namespace SuperTiendaCustomers.API.DependencyInjections
{
    /// <summary>
    /// Meditator patterns dependencies
    /// </summary>
    public class MediatorDependencies
    {
        /// <summary>
        /// Method to add dependencies
        /// </summary>
        /// <param name="services">App services</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Exception</exception>
        public static IServiceCollection AddDependencies(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            // Add Logger Error Response Pipeline Behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerErrorResponseBehavior<,>));

            return services;
        }
    }
}
