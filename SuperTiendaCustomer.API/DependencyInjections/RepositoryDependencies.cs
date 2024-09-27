using SuperTiendaCustomer.Domain.Interfaces.Repositories;
using SuperTiendaCustomer.Infrastructure.Repositories;

namespace SuperTiendaCustomers.API.DependencyInjections
{
    /// <summary>
    /// Repository patterns dependencies
    /// </summary>
    public class RepositoryDependencies
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

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped(typeof(IEntityFrameworkBuilder<>),
            typeof(EntityFrameworkBuilder<>));

            return services;
        }
    }
}
