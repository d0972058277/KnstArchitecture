using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Queries;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Data.SqlClient;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddKnstArchitectureSqlServer(this IServiceCollection services)
        {
            services.TryAddKnstArchitecture();
            services.TryAddKnstDbContexts();
            services.AddTransient<ISqlServerDbSession, SqlServerDbSession>();
            services.AddScoped<ISqlServerUnitOfWork, SqlServerUnitOfWork>();
            
            services.AddTransient<IEFCoreDbSession>(sp => sp.GetRequiredService<ISqlServerDbSession>());
            services.AddTransient<ISqlDbSession>(sp => sp.GetRequiredService<ISqlServerDbSession>());

            services.AddScoped<IEFCoreUnitOfWork>(sp => sp.GetRequiredService<ISqlServerUnitOfWork>());
            services.AddScoped<ISqlUnitOfWork>(sp => sp.GetRequiredService<ISqlServerUnitOfWork>());

            return services;
        }

        public static IServiceCollection AddKnstArchitectureSqlServerWithQuery(this IServiceCollection services, string slaverConnectionString)
        {
            services.AddKnstArchitectureSqlServer();
            services.TryAddKnstArchitectureSqlQueries();
            services.AddSingleton(new SqlServerQueryConnectionFactory(() =>
            {
                return new SqlConnection(slaverConnectionString);
            }));
            return services;
        }

        public static IServiceCollection AddKnstArchitectureSqlServerWithQuery(this IServiceCollection services, Func<SqlConnection> func)
        {
            services.AddKnstArchitectureSqlServer();
            services.TryAddKnstArchitectureSqlQueries();
            services.AddSingleton(new SqlServerQueryConnectionFactory(func));
            return services;
        }
    }
}