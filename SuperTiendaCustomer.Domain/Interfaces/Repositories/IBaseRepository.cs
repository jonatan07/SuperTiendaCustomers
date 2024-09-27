using SuperTiendaCustomer.Domain.Entities;
using SuperTiendaCustomer.Domain.Responses.PaginationResponse;
using SuperTiendaCustomer.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> GetByIdAsync(int id);

        Task<GetRecordsResponse<T>> GetAll(Specification<T>? specification = null);
        Task<int> Count(List<Criterion<T>>? specifications = null);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);
        void BulkInsert(List<T> entities);
        Task BulkDeleteAsync(List<T> entities);

        void ExecuteProcedure(string name, List<string> parameters);

        Task<int> ExecuteProcedureAsync(string sql);


    }
}
