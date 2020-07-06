using System;
using System.Data;
using System.Data.Common;
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
            services.AddTransient<ISqlServerDbSession, SqlServerDbSession>(sp =>
            {
                var bag = sp.GetRequiredService<IDbSessionBag>();

                var idbconn = sp.GetService<IDbConnection>();
                var dbconn = sp.GetService<DbConnection>();
                var providerconn = sp.GetService<SqlConnection>();

                if (!(providerconn is null))
                {
                    return new SqlServerDbSession(bag, providerconn, sp);
                }

                if (!(dbconn is null) && dbconn is SqlConnection)
                {
                    return new SqlServerDbSession(bag, dbconn, sp);
                }

                if (!(idbconn is null) && idbconn is SqlConnection)
                {
                    return new SqlServerDbSession(bag, idbconn, sp);
                }

                throw new InvalidOperationException($"The type of {nameof(SqlConnection)} service is not registered.");
            });
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