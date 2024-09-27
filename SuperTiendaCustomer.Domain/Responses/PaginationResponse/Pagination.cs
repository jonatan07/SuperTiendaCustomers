using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Domain.Responses.PaginationResponse
{
    public class Pagination
    {
        public int Page { get; }
        public int PageSize { get; }
        public int TotalRecords { get; }
        public int TotalPages
        {
            get
            {
                if (TotalRecords == 0 || PageSize == 0)
                {
                    return 0;
                }

                var total = (int)Math.Ceiling(TotalRecords / (double)PageSize);

                return total;
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return Page > 0;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return Page < TotalPages - 1;
            }
        }

        public Pagination()
        {
            Page = 0;
            PageSize = 0;
            TotalRecords = 0;
        }

        public Pagination(int page, int pageSize, int totalRecords)
        {
            Page = page;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }
    }
}
