using MediatR;
using Microsoft.Extensions.Logging;
using SuperTiendaCustomer.Domain.Responses.FlowResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Application.Behaviors
{
    public class LoggerErrorResponseBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggerErrorResponseBehavior<TRequest, TResponse>> _logger;

        public LoggerErrorResponseBehavior(ILogger<LoggerErrorResponseBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var handlerResponse = await next();

            if (handlerResponse is Response { IsSuccess: false } response)
            {

                foreach (var errorResponse in response.Errors)
                {
                    _logger.LogError("{Code}: {Detail}",
                        errorResponse.Code,
                        errorResponse.Detail);
                }
            }

            return handlerResponse;
        }
    }
}
