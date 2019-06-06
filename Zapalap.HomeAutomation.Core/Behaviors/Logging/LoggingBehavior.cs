using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zapalap.HomeAutomation.Core.Behaviors.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Debug.WriteLine($"Recieved {request.GetType().Name} with payload: {GetFormattedPayload(request)}");

            var result = await next();

            Debug.WriteLine($"{request.GetType().Name} returned a result: {GetFormattedPayload(result)}");
            return result;
        }

        private string GetFormattedPayload<T>(T instance)
        {
            var instanceType = typeof(T);

            var requestProps = instanceType
                .GetProperties()
                .Select(p => (p.Name, p.GetValue(instance)))
                .ToDictionary(k => k.Name, v => v.Item2);

            return string.Join(", ", requestProps.Select(p => $"{p.Key}: {p.Value}"));
        }
    }
}
