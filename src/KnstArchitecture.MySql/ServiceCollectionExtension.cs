using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Queries;
using KnstArchitecture.UnitOfWorks;
using MySql.Data.MySqlClient;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddKnstArchitectureMySql(this IServiceCollection services)
        {
            services.TryAddKnstArchitecture();
            services.TryAddKnstDbContexts();
            services.AddTransient<IMySqlDbSession, MySqlDbSession>();
            services.AddScoped<IMySqlUnitOfWork, MySqlUnitOfWork>();

            services.AddTransient<IEFCoreDbSession>(sp => sp.GetRequiredService<IMySqlDbSession>());
            services.AddTransient<ISqlDbSession>(sp => sp.GetRequiredService<IMySqlDbSession>());

            services.AddScoped<IEFCoreUnitOfWork>(sp => sp.GetRequiredService<IMySqlUnitOfWork>());
            services.AddScoped<ISqlUnitOfWork>(sp => sp.GetRequiredService<IMySqlUnitOfWork>());

            return services;
        }

        public static IServiceCollection AddKnstArchitectureMySqlWithQuery(this IServiceCollection services, string slaverConnectionString)
        {
            services.AddKnstArchitectureMySql();
            services.TryAddKnstArchitectureSqlQueries();
            services.AddSingleton(new MySqlQueryConnectionFactory(() =>
            {
                return new MySqlConnection(slaverConnectionString);
            }));
            return services;
        }

        public static IServiceCollection AddKnstArchitectureMySqlWithQuery(this IServiceCollection services, Func<MySqlConnection> func)
        {
            services.AddKnstArchitectureMySql();
            services.TryAddKnstArchitectureSqlQueries();
            services.AddSingleton(new MySqlQueryConnectionFactory(func));
            return services;
        }
    }
}