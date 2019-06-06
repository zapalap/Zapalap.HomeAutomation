using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zapalap.HomeAutomation.Core.Behaviors;
using Zapalap.HomeAutomation.Core.Behaviors.Logging;
using Zapalap.HomeAutomation.Core.Behaviors.Validation;
using Zapalap.HomeAutomation.Core.Behaviors.Validation.Validators;

namespace Zapalap.HomeAutomation.WebApi.Config
{
    public static class SimpleInjectorConfig
    {
        public static void RegisterServicesWithSimpleInjector(this IServiceCollection services, Container container)
        {
            //Wire SimpleInjector as controller activator
            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });

            RegisterAppDependencies(container);
        }

        //Add SimpleInjector and verify container
        public static void UseSimpleInjectorAndVerify(this IApplicationBuilder app, Container container)
        {
            app.UseSimpleInjector(container);
            container.Verify();
        }

        private static void RegisterAppDependencies(Container container)
        {
            // ServiceFactory used by the MediatR to resolve instances
            container.RegisterSingleton(() => new ServiceFactory(container.GetInstance));

            // The Mediator itself
            container.RegisterSingleton<IMediator, Mediator>();

            //Scan the core assembly and register or handlers
            container.Register(typeof(IRequestHandler<,>), typeof(IValidator<>).Assembly);

            //Register pipeline behaviors in the order they are added
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[] {
                typeof(JsonLoggingBehavior<,>),
                typeof(InputValidatingBehavior<,>),
                });

            //Register validator collection for the InputValidatingBehavior. Supports Contravariance.
            container.Collection.Register(typeof(IValidator<>), typeof(IValidator<>).Assembly);
        }
    }
}
