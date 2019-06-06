using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zapalap.HomeAutomation.Core.Behaviors;
using Zapalap.HomeAutomation.Core.Behaviors.Validators;

namespace Zapalap.HomeAutomation.WebApi.Config
{
    public static class SimpleInjectorConfig
    {
        public static void AddSimpleInjectorDefaults(this IServiceCollection services, Container container)
        {
            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });
        }

        public static void RegisterServicesWithSimpleInjectorAndVerify(this IApplicationBuilder app, Container container)
        {
            app.UseSimpleInjector(container);
            RegisterAppDependencies(container);

            container.Verify();
        }

        private static void RegisterAppDependencies(Container container)
        {
            container.RegisterSingleton(() => new ServiceFactory(container.GetInstance));
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), typeof(IValidator<>).Assembly);

            container.Collection.Register(typeof(IPipelineBehavior<,>), new[] {
                typeof(JsonLoggingBehavior<,>),
                typeof(InputValidatingBehavior<,>),
                });

            container.Collection.Register(typeof(IValidator<>), typeof(IValidator<>).Assembly);
        }
    }
}
