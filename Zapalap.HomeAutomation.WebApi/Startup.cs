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
using Zapalap.HomeAutomation.Core.Behaviors.Logging;
using Zapalap.HomeAutomation.Core.Behaviors.Validation;
using Zapalap.HomeAutomation.Core.Behaviors.Validation.Exceptions;
using Zapalap.HomeAutomation.Core.Behaviors.Validation.Validators;
using Zapalap.HomeAutomation.Core.Features.Doors.Commands.OpenDoor;
using Zapalap.HomeAutomation.WebApi.Config;
using Zapalap.HomeAutomation.WebApi.Infrastructure.ServiceCollectionExtensions;

namespace Zapalap.HomeAutomation.WebApi
{
    public class Startup
    {
        //private Container Container = new Container();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.RegisterServicesWithMicrosoftDependencyInjection();
            //services.RegisterServicesWithSimpleInjector(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UsePipelineErrorHandling();
            app.UseMvc();

            //app.UseSimpleInjectorAndVerify(Container);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
