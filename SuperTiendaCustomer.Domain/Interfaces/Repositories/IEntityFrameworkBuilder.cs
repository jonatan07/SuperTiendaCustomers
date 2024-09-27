using SuperTiendaCustomer.Domain.Entities;
using SuperTiendaCustomer.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Domain.Interfaces.Repositories
{
    public interface IEntityFrameworkBuilder<T> where T : BaseEntity
    {
        Expression<Func<T, bool>>? GetWhereExpression(List<Criterion<T>> criteria);

        Task<List<T>> ToListOrderedPagedValues(IQueryable<T> query, Specification<T> specification);
    }
}
