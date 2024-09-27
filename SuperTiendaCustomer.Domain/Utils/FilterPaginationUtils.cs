using SuperTiendaCustomer.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Domain.Utils
{
    public static class FilterPaginationUtils
    {
        public static List<Filter> ConstructFilters(List<string>? filters, List<Filter>? customFilters = null)
        {
            var result = new List<Filter>();
            if (filters == null || !filters.Any())
            {
                if (customFilters != null)
                {
                    result.AddRange(customFilters);
                }

                return result;
            }

            foreach (var filterString in filters)
            {
                var parameterList = filterString.Split(',').ToList();
                if (parameterList.Count != 4)
                {
                    throw new ApplicationException("Filters are not well formatted. " +
                                                   "They should follow this format: \"field,value,operator,comparer\". " +
                                                   $"Possible values for operator are: {FilterOperator.List()}. " +
                                                   $"Possible values for comparer are: {FilterComparer.List()}. ");
                }

                if (customFilters != null && customFilters.Any(f => f.Field.Equals(parameterList[0])))
                {
                    continue;
                }

                var value = parameterList[1];
                Filter filter;
                if (value.ToLower().StartsWith("list(") && parameterList[2].ToLower().Contains("contains"))
                {
                    value = value.Substring(5, value.Length - 6);
                    var valueList = value.Split(';').ToList();

                    if (Guid.TryParse(valueList.First(), out _))
                    {
                        var valueListGuidObject = valueList.Select(Guid.Parse).ToList();
                        filter = new Filter(parameterList[0], FilterOperator.FromName(parameterList[2]),
                            FilterComparer.FromName(parameterList[3]), valueListGuidObject);
                    }
                    else
                    {
                        filter = new Filter(parameterList[0], FilterOperator.FromName(parameterList[2]),
                            FilterComparer.FromName(parameterList[3]), valueList);
                    }
                }
                else if (value.ToLower().StartsWith("datetime("))
                {
                    value = value.Substring(9, value.Length - 10);
                    var valueDateTime = DateTime.Parse(value);
                    filter = new Filter(parameterList[0], FilterOperator.FromName(parameterList[2]),
                        FilterComparer.FromName(parameterList[3]), valueDateTime);
                }
                else
                {
                    filter = new Filter(parameterList[0], FilterOperator.FromName(parameterList[2]),
                        FilterComparer.FromName(parameterList[3]), value);
                }

                result.Add(filter);
            }

            if (customFilters != null)
            {
                result.AddRange(customFilters);
            }

            return result;
        }

        public static List<OrderBy> ConstructOrders(List<string>? orders)
        {
            var result = new List<OrderBy>();
            if (orders == null || !orders.Any())
            {
                return result;
            }

            foreach (var orderString in orders)
            {
                var parameters = orderString.Split(',').ToList();
                if (parameters.Count != 2)
                {
                    throw new ApplicationException("Orders are not well formatted. " +
                                                   "They should follow this format: \"field,orderType\". " +
                                                   $"Possible values for orderType are: {OrderType.List()}. ");
                }

                var orderBy = new OrderBy(parameters[0], OrderType.FromName(parameters[1]));
                result.Add(orderBy);
            }

            return result;
        }
    }
}
