using Microsoft.Extensions.DependencyInjection.Extensions;
using RoutineTracker.Server.Abstractions;
using System.Reflection;

namespace RoutineTracker.Server.Extensions
{
    public static class EndpointExtension
    {
        public static IServiceCollection AddEndpoints (this IServiceCollection services)
        {
            services.AddEndpoints(Assembly.GetExecutingAssembly());
            return services;

        }

        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
            ServiceDescriptor[] serviceDescriptors = assembly.DefinedTypes.Where(type => type is { IsAbstract: false, IsInterface: false } &&
            type.IsAssignableTo(typeof(IEndpoint))
            )
             .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
             .ToArray();

            services.TryAddEnumerable(serviceDescriptors);
            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app)
        {
            IEnumerable<IEndpoint> services = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
            foreach (var service in services)
            {
                service.MapEndpoints(app);
            }
            return app;
        }
    }

    
}
