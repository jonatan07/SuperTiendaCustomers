using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Domain.Responses.FlowResponse
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Detail { get; set; }

        public ErrorResponse(string code, string detail)
        {
            Code = code;
            Detail = detail;
        }

        public ErrorResponse(string detail)
        {
            Detail = detail;
        }
    }
}
