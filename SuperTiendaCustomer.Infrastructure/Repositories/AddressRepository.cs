using SuperTiendaCustomer.Domain.Entities;
using SuperTiendaCustomer.Domain.Interfaces.Repositories;
using SuperTiendaCustomer.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SuperTiendaCustomer.Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(DataDbContext context, IEntityFrameworkBuilder<Address> entityFrameworkBuilder)
           : base(context, entityFrameworkBuilder)
        {
        }
    }
}
