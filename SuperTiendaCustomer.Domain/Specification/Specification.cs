using System.Linq.Expressions;

namespace SuperTiendaCustomer.Domain.Specification
{
    public class Specification<T> where T : class
    {
        public List<Criterion<T>> Criteria { get; }
        public List<Order<T>> Orders { get; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public List<Expression<Func<T, object>>> Includes { get; }
        public List<string> IncludeStrings { get; }
        public Expression<Func<T, bool>>? AdditionalRawWhere { get; }

        public Specification(List<Filter>? filters = null, List<OrderBy>? orderByList = null,
            int? page = null, int? pageSize = null,
            List<Expression<Func<T, object>>>? includes = null, List<string>? includeStrings = null,
            Expression<Func<T, bool>>? additionalRawWhere = null, bool maxPageSize = false)
        {
            filters ??= new List<Filter>();
            Criteria = GetCriteria(filters);
            orderByList ??= new List<OrderBy>();
            Orders = GetOrders(orderByList);
            Page = page ?? 0;
            if (!maxPageSize)
                PageSize = pageSize == null || pageSize > Constants.MaxPagesize ? Constants.MaxPagesize : (int)pageSize;
            else
                PageSize = pageSize == null ? Constants.MaxPagesize : (int)pageSize;

            Includes = includes ?? new List<Expression<Func<T, object>>>();
            IncludeStrings = includeStrings ?? new List<string>();
            AdditionalRawWhere = additionalRawWhere;
        }

        private static List<Criterion<T>> GetCriteria(IEnumerable<Filter> filters)
        {
            return filters
                .Select(filter => new Criterion<T>(
                        new FilterField<T>(filter.Field), filter.Operator, filter.Comparer, filter.Value)).ToList();
        }

        private static List<Order<T>> GetOrders(IEnumerable<OrderBy> orderByList)
        {
            return orderByList.Select(orderBy => new Order<T>(
                    new OrderField<T>(orderBy.Field), orderBy.OrderType)).ToList();
        }

        public void SetPage(int value)
        {
            Page = value;
        }

        public void SetPageSize(int value)
        {
            PageSize = value;
        }
    }
}
