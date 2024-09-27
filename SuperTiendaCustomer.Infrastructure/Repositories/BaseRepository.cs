using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SuperTiendaCustomer.Domain.Entities;
using SuperTiendaCustomer.Domain.Interfaces.Repositories;
using SuperTiendaCustomer.Domain.Responses.PaginationResponse;
using SuperTiendaCustomer.Domain.Specification;
using SuperTiendaCustomer.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataDbContext _context;
        private readonly IEntityFrameworkBuilder<T> _entityFrameworkBuilder;

        public BaseRepository(DataDbContext context, IEntityFrameworkBuilder<T> entityFrameworkBuilder)
        {
            _context = context;
            _entityFrameworkBuilder = entityFrameworkBuilder;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<int> Count(List<Criterion<T>>? specifications = null)
        {
            var baseQuery = _context.Set<T>().AsQueryable();

            if (specifications == null)
            {
                return await baseQuery.CountAsync();
            }

            var whereExpression = _entityFrameworkBuilder.GetWhereExpression(specifications);
            if (whereExpression != null)
            {
                baseQuery = baseQuery.Where(whereExpression);
            }

            return await baseQuery.CountAsync();
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<GetRecordsResponse<T>> GetAll(Specification<T>? specification = null)
        {
            specification ??= new Specification<T>();

            //Get the total amount of entities
            var totalAmount = await Count(specification.Criteria);

            //If there is no entities return empty list of entities.
            if (totalAmount == 0)
            {
                return new GetRecordsResponse<T>(new List<T>(), new Pagination());
            }

            //valid max page size by total amount.
            if (totalAmount < specification.PageSize)
            {
                specification.SetPageSize(totalAmount);
            }

            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = specification.Includes
                .Aggregate(_context.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = specification.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            var whereExpression = _entityFrameworkBuilder.GetWhereExpression(specification.Criteria);
            if (whereExpression != null)
            {
                secondaryResult = secondaryResult.Where(whereExpression);
            }

            if (specification.AdditionalRawWhere != null)
            {
                secondaryResult = secondaryResult.Where(specification.AdditionalRawWhere);
            }

            var entities = await _entityFrameworkBuilder.ToListOrderedPagedValues(secondaryResult, specification);

            var pagination = new Pagination(specification.Page, specification.PageSize, totalAmount);

            return new GetRecordsResponse<T>(entities, pagination);
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public virtual void BulkInsert(List<T> entities)
        {
            _context.BulkInsert<T>(entities, options =>
            {
                options.BatchSize = 15000;
                options.BulkCopyTimeout = 60;
            });
        }
        public async Task BulkDeleteAsync(List<T> entities)
        {
            await _context.BulkDeleteAsync<T>(entities, options =>
            {
                options.BatchSize = 15000;
                options.BulkCopyTimeout = 60;
            });
        }

        public virtual void Update(T entity)
        {
            entity.SetUpdatedAt();
            _context.Set<T>()
                .Update(entity);
        }

        public virtual void ExecuteProcedure(string name, List<string> parameters)
        {
            _context.Database.ExecuteSqlInterpolated($"{name} {string.Join(",", parameters)}");
        }
        public async Task<int> ExecuteProcedureAsync(string sql)
        {
            var result = await _context.Database.ExecuteSqlRawAsync(sql);

            return result;
        }

    }
}
