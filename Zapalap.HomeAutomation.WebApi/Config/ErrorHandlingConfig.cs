using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zapalap.HomeAutomation.Core.Behaviors.Validation.Exceptions;

namespace Zapalap.HomeAutomation.WebApi.Config
{
    public static class ErrorHandlingConfig
    {
        public static void UsePipelineErrorHandling(this IApplicationBuilder app)
        {
            // Input validation error handling middleware
            app.Use((context, next) =>
            {
                try
                {
                    return next();
                }
                catch (InputValidationException ex)
                {
                    if(!context.Response.HasStarted)
                    {
                        context.Response.Clear();
                        context.Response.StatusCode = 400;
                        context.Response.WriteAsync(ex.Message);
                    }

                    return Task.FromResult(0);
                }
            });
        }
    }
}
