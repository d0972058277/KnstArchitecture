using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Queries;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Data.Sqlite;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddKnstArchitectureSqlite(this IServiceCollection services)
        {
            services.TryAddKnstArchitecture();
            services.TryAddKnstDbContexts();
            services.AddTransient<ISqliteDbSession, SqliteDbSession>();
            services.AddScoped<ISqliteUnitOfWork, SqliteUnitOfWork>();

            services.AddTransient<IEFCoreDbSession>(sp => sp.GetRequiredService<ISqliteDbSession>());
            services.AddTransient<ISqlDbSession>(sp => sp.GetRequiredService<ISqliteDbSession>());

            services.AddScoped<IEFCoreUnitOfWork>(sp => sp.GetRequiredService<ISqliteUnitOfWork>());
            services.AddScoped<ISqlUnitOfWork>(sp => sp.GetRequiredService<ISqliteUnitOfWork>());

            return services;
        }

        public static IServiceCollection AddKnstArchitectureSqliteWithQuery(this IServiceCollection services, string slaverConnectionString)
        {
            services.AddKnstArchitectureSqlite();
            services.TryAddKnstArchitectureSqlQueries();
            services.AddSingleton(new SqliteQueryConnectionFactory(() =>
            {
                return new SqliteConnection(slaverConnectionString);
            }));
            return services;
        }

        public static IServiceCollection AddKnstArchitectureSqliteWithQuery(this IServiceCollection services, Func<SqliteConnection> func)
        {
            services.AddKnstArchitectureSqlite();
            services.TryAddKnstArchitectureSqlQueries();
            services.AddSingleton(new SqliteQueryConnectionFactory(func));
            return services;
        }
    }
}