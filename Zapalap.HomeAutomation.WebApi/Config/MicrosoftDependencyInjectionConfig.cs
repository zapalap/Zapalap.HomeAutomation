using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zapalap.HomeAutomation.Core.Behaviors.Logging;
using Zapalap.HomeAutomation.Core.Behaviors.Validation;
using Zapalap.HomeAutomation.Core.Behaviors.Validation.Validators;
using Zapalap.HomeAutomation.Core.Features.Doors.Commands.OpenDoor;
using Zapalap.HomeAutomation.WebApi.Infrastructure.ServiceCollectionExtensions;

namespace Zapalap.HomeAutomation.WebApi.Config
{
    public static class MicrosoftDependencyInjectionConfig
    {
        public static void RegisterServicesWithMicrosoftDependencyInjection(this IServiceCollection services)
        {
            services.AddMediatR(typeof(OpenDoor).Assembly);

            // Mediator Pipline Behaviors
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(JsonLoggingBehavior<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(InputValidatingBehavior<,>));
            //services.AddCollection(typeof(IValidator<>), new[] { typeof(IValidator<>).Assembly }, ServiceLifetime.Scoped);
        }
    }
}
