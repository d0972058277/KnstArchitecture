using System;
using System.Data;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Queries;
using KnstArchitecture.UnitOfWorks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddKnstArchitectureSql(this IServiceCollection services)
        {
            services.TryAddKnstArchitecture();
            services.AddTransient<ISqlDbSession, SqlDbSession>();
            services.AddScoped<ISqlUnitOfWork, SqlUnitOfWork>();
            return services;
        }

        public static IServiceCollection AddKnstArchitectureSqlWithQuery(this IServiceCollection services, Func<IDbConnection> func)
        {
            services.AddKnstArchitectureSql();
            services.TryAddKnstArchitectureSqlQueries();
            services.AddSingleton<IQueryConnectionFactory, SqlQueryConnectionFactory>(sp => new SqlQueryConnectionFactory(func));
            return services;
        }

        public static IServiceCollection TryAddKnstArchitectureSqlQueries(this IServiceCollection services)
        {
            services.TryAddAllTypes<IQuery>(ServiceLifetime.Transient);
            return services;
        }
    }
}