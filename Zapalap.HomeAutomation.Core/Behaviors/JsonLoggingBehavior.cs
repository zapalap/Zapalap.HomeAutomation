using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zapalap.HomeAutomation.Core.Behaviors
{
    public class JsonLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var payloadJson = JsonConvert.SerializeObject(request, Formatting.Indented);
            Debug.WriteLine($"Incoming {request.GetType().Name} {payloadJson}");

            var result = await next();

            var resultJson = JsonConvert.SerializeObject(result, Formatting.Indented);

            Debug.WriteLine($"Got result from {request.GetType().Name} {resultJson}");

            return result;
        }
    }
}
