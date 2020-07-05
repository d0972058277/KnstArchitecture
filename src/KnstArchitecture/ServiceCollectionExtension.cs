using System;
using System.Linq;
using KnstArchitecture;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using KnstArchitecture.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection TryAddKnstArchitecture(this IServiceCollection services)
        {
            services.TryAddKnstArchitectureDbSessionBag();
            services.TryAddKnstArchitectureRepos();
            services.TryAddKnstArchitectureServices();
            services.TryAddKnstArchitectureLazy();

            return services;
        }

        public static IServiceCollection TryAddKnstArchitectureDbSessionBag(this IServiceCollection services)
        {
            services.TryAddScoped<IDbSessionBag, DbSessionBag>();
            return services;
        }

        public static IServiceCollection AddAllTypes<T>(this IServiceCollection services, ServiceLifetime lifetime)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T)) && !x.IsInterface && !x.IsAbstract)).ToList();
            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces().ToList();
                foreach (var @interface in interfaces)
                {
                    services.Add(new ServiceDescriptor(@interface, type, lifetime));
                }

            }
            return services;
        }

        public static IServiceCollection TryAddAllTypes<T>(this IServiceCollection services, ServiceLifetime lifetime)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T)) && !x.IsInterface && !x.IsAbstract)).ToList();
            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces().ToList();
                foreach (var @interface in interfaces)
                {
                    services.TryAdd(new ServiceDescriptor(@interface, type, lifetime));
                }
            }
            return services;
        }

        public static IServiceCollection TryAddKnstArchitectureRepos(this IServiceCollection services)
        {
            services.TryAddAllTypes<IRepo>(ServiceLifetime.Transient);
            return services;
        }

        public static IServiceCollection TryAddKnstArchitectureServices(this IServiceCollection services)
        {
            services.TryAddAllTypes<IService>(ServiceLifetime.Scoped);
            return services;
        }

        public static IServiceCollection TryAddKnstArchitectureLazy(this IServiceCollection services)
        {
            services.TryAddTransient(typeof(Lazy<>), typeof(Lazier<>));
            return services;
        }
    }
}