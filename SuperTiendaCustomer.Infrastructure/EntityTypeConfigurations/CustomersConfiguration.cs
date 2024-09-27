using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTiendaCustomer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Infrastructure.EntityTypeConfigurations
{
    public class CustomersConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            /*
            builder
                .Property(b => b.Date)
                .IsRequired();

            builder
                .Property(b => b.StartPeriod)
                 .HasMaxLength(7);

            builder
             .Property(b => b.EndPeriod)
              .HasMaxLength(7);



            builder.HasOne(e => e.PluginDataLoad)
                   .WithOne(b => b.DataLoad)
                   .HasForeignKey<PluginDataLoad>(e => e.DataLoadId);
            */
            builder.HasKey(b => b.Id);

        }
    }
}
