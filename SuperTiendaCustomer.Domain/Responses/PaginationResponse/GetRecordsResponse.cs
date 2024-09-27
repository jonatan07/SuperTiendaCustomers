using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Domain.Responses.PaginationResponse
{
    public class GetRecordsResponse<T>
    {
        public IReadOnlyCollection<T> Records { get; set; }
        public Pagination Pagination { get; set; }

        public GetRecordsResponse(IReadOnlyCollection<T> records, Pagination pagination)
        {
            Records = records;
            Pagination = pagination;
        }

        public GetRecordsResponse()
        {
            Records = new List<T>();
            Pagination = new Pagination();
        }
    }
}
