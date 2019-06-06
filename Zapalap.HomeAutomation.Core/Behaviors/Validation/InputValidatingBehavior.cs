using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zapalap.HomeAutomation.Core.Behaviors.Validation.Validators;
using Zapalap.HomeAutomation.Core.Helpers.Results;

namespace Zapalap.HomeAutomation.Core.Behaviors.Validation
{
    public class InputValidatingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    {
        private readonly IEnumerable<IValidator<TRequest>> Validators;

        public InputValidatingBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            Validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationResults = Validators.Select(v => v.Validate(request)).ToList();

            if (validationResults.Any(v => v.InputIsInvalid))
            {
                var messages = string.Join(" | ", validationResults.Select(v => v.Message));
                Debug.WriteLine($"Validation failed: {messages}");
                throw new InvalidOperationException(messages);
            }

            return await next();
        }
    }
}
