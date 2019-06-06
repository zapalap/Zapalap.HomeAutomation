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
        //This allows to scan the assembly and register all types matching a generic interface as collection
        public static void AddCollection(this IServiceCollection services, Type serviceType, Assembly[] assemblies, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            //Find all generic types in all given assemblies that match the given serviceType
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
