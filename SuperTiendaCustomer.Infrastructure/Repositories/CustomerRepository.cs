using SuperTiendaCustomer.Domain.Entities;
using SuperTiendaCustomer.Domain.Interfaces.Repositories;
using SuperTiendaCustomer.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataDbContext context, IEntityFrameworkBuilder<Customer> entityFrameworkBuilder)
           : base(context, entityFrameworkBuilder)
        {
        }
    }
}
