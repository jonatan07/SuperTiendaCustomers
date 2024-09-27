using Microsoft.EntityFrameworkCore;
using SuperTiendaCustomer.Infrastructure.Context;

namespace SuperTiendaCustomers.API.Extensions
{
    public static class HostDatabaseExtension
    {
        /// <summary>
        /// Init database method
        /// </summary>
        /// <param name="services">App services</param>
        public static void InitDatabase(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataDbContext>();
                db.Database.Migrate();

                // -------------
                //  Seeders
                // -------------
            }
        }
    }
}
