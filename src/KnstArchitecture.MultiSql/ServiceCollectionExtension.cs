using System;
using System.Data;
using System.Linq;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using KnstArchitecture.Repos.Factories;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddKnstArchitectureMultiSql(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.DefinedTypes
                    .Where(x => x
                        .GetInterfaces()
                        .Contains(typeof(IRepo)) && !x.IsInterface && !x.IsAbstract))
                .ToList();
                
            foreach (var type in types)
            {
                if (typeof(SqlRepo).IsAssignableFrom(type))
                {
                    continue;
                }

                services.TryAdd(new ServiceDescriptor(type, type, ServiceLifetime.Transient));

                var interfaces = type.GetInterfaces().ToList();
                foreach (var @interface in interfaces)
                {
                    services.TryAdd(new ServiceDescriptor(@interface, sp => sp.GetRequiredService(type), ServiceLifetime.Transient));
                }
            }

            services.TryAddKnstArchitectureDbSessionBag();
            services.TryAddKnstArchitectureLazy();
            services.AddTransient<IMultiSqlDbSession, MultiSqlDbSession>();
            services.AddScoped<IMultiSqlUnitOfWork, MultiSqlUnitOfWork>();

            services.AddTransient<ISqlRepoFactory, SqlRepoFactory>();

            services.AddTransient<RichSqlDbSession>(sp =>
            {
                var result = new RichSqlDbSession();
                var dbSessionBag = sp.GetRequiredService<IDbSessionBag>();
                var connections = sp.GetServices<IDbConnection>().ToList();

                foreach (var connection in connections)
                {
                    var session = new SqlDbSession(dbSessionBag, connection);
                    result.Add(session);
                }

                return result;
            });

            return services;
        }
    }
}