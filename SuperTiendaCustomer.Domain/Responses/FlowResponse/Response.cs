using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Domain.Responses.FlowResponse
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public List<ErrorResponse> Errors { get; set; }

        public Response() { }

        public Response(List<ErrorResponse> errors)
        {
            IsSuccess = false;
            Errors = errors;
        }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }

        public Response(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        public Response(List<ErrorResponse> errors)
        {
            IsSuccess = false;
            Errors = errors;
        }
    }
}
