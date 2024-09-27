using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SuperTiendaCustomer.Domain.Entities;
using SuperTiendaCustomer.Domain.Interfaces.Repositories;
using SuperTiendaCustomer.Infrastructure.EntityTypeConfigurations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Pluralization;

namespace SuperTiendaCustomer.Infrastructure.Context
{
    public class DataDbContext : Microsoft.EntityFrameworkCore.DbContext,IUnitOfWork
    {
        protected readonly IConfiguration Configuration;

        private readonly string PrefixDb = "customer";
        public DataDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Tomamos el string conexion de nuestro appsetting a nivel local esto para pruebas
            var connectionString = Configuration.GetConnectionString("DatabaseConnection");
            // Tomamos los parametros de conexion que son injectado de los ambiente QA y PROD
            var databasePassword = Environment.GetEnvironmentVariable("DATABASE_PASSWD");
            var databaseUser = Environment.GetEnvironmentVariable("DATABASE_USER");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseSchema = Environment.GetEnvironmentVariable("DATABASE_SCHEMA");
            // Validamos los parametros y formamos nuestro cadena de conexion.
            if (!string.IsNullOrEmpty(databasePassword) && !string.IsNullOrEmpty(databaseUser) &&
            !string.IsNullOrEmpty(databaseUrl) && !string.IsNullOrEmpty(databaseSchema))
            {
                connectionString =
                    $"Data Source={databaseUrl}; Initial Catalog={databaseSchema}; User Id={databaseUser}; Password={databasePassword}; TrustServerCertificate=true";
            }
            // Configuramos la conexion, politica de reintento y migraciones
            options.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                builder.MigrationsHistoryTable($"{PrefixDb}__EFMigrationsHistory");
            });
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CustomersConfiguration());
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (!entity.IsKeyless)
                    modelBuilder.Entity(entity.Name).ToTable($"{PrefixDb}_" + DbConfiguration.DependencyResolver.GetService<IPluralizationService>().Pluralize(entity.GetTableName()));
            }
        }
        public virtual Microsoft.EntityFrameworkCore.DbSet<Address> Address { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<Customer> Customers { get; set; }
    }
}