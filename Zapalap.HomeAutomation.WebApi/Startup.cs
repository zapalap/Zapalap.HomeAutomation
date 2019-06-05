using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using Zapalap.HomeAutomation.Core.Behaviors;
using Zapalap.HomeAutomation.Core.Behaviors.Validators;
using Zapalap.HomeAutomation.Core.Features.Doors.Commands.OpenDoor;
using Zapalap.HomeAutomation.WebApi.Infrastructure.ServiceCollectionExtensions;

namespace Zapalap.HomeAutomation.WebApi
{
    public class Startup
    {
        private Container Container = new Container();

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMediatR(typeof(OpenDoor).Assembly);
            services.AddMvc();
            services.AddSimpleInjector(Container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });

            // Mediator Pipline Behaviors
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(JsonLoggingBehavior<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(InputValidatingBehavior<,>));
            //services.AddCollection(typeof(IValidator<>), new[] { typeof(IValidator<>).Assembly }, ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSimpleInjector(Container);

            RegisterDependenciesWithContainer();

            Container.Verify();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private void RegisterDependenciesWithContainer()
        {
            Container.RegisterSingleton(() => new ServiceFactory(Container.GetInstance));
            Container.RegisterSingleton<IMediator, Mediator>();
            Container.Register(typeof(IRequestHandler<,>), typeof(IValidator<>).Assembly);

            Container.Collection.Register(typeof(IPipelineBehavior<,>), new[] {
                typeof(JsonLoggingBehavior<,>),
                typeof(InputValidatingBehavior<,>),
                });

            var typesToRegister = Container.GetTypesToRegister(typeof(IValidator<>), new[] { typeof(IValidator<>).Assembly }, new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false
            });

            Container.Collection.Register(typeof(IValidator<>), typesToRegister);
        }
    }
}
