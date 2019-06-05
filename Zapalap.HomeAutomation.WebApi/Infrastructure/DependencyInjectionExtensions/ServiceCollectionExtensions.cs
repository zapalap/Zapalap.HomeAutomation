using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Zapalap.HomeAutomation.WebApi.Infrastructure.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCollection(this IServiceCollection services, Type serviceType, Assembly[] assemblies, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            var typesToRegister = assemblies
                .SelectMany(a => a.GetTypes()
                    .Where(x => x.GetInterfaces().Any(t => (t.IsGenericType && t.GetGenericTypeDefinition() == serviceType) || (t == serviceType))));

            foreach (var type in typesToRegister)
            {
                var implementationGenericArguments = type.GetInterfaces().FirstOrDefault(t => (t.IsGenericType && t.GetGenericTypeDefinition() == serviceType) || (t == serviceType)).GetGenericArguments();
                var closedGenericServiceType = serviceType.MakeGenericType(implementationGenericArguments);

                services.Add(new ServiceDescriptor(closedGenericServiceType, type, serviceLifetime));
            }
        }
    }
}
