using SuperTiendaCustomer.Domain.Specification;
using SuperTiendaCustomer.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Domain.Models
{
    public class FilterPaginationQueryModel
    {
        public List<string>? Filters { get; set; }
        public List<string>? OrderBy { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }

        private readonly List<Filter> _customFilters;

        public FilterPaginationQueryModel()
        {
            _customFilters = new List<Filter>();
        }

        public List<Filter> GetFilters()
        {
            return FilterPaginationUtils.ConstructFilters(Filters, _customFilters);
        }

        public List<OrderBy> GetOrders()
        {
            return FilterPaginationUtils.ConstructOrders(OrderBy);
        }

        public void AddFilters(IEnumerable<Filter> filters)
        {
            _customFilters.AddRange(filters);
        }
    }
}
