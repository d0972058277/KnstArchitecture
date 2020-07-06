using System;
using System.Data;
using System.Data.Common;
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
            services.AddTransient<IMySqlDbSession, MySqlDbSession>(sp =>
            {
                var bag = sp.GetRequiredService<IDbSessionBag>();

                var idbconn = sp.GetService<IDbConnection>();
                var dbconn = sp.GetService<DbConnection>();
                var providerconn = sp.GetService<MySqlConnection>();

                if (!(providerconn is null))
                {
                    return new MySqlDbSession(bag, providerconn, sp);
                }

                if (!(dbconn is null) && dbconn is MySqlConnection)
                {
                    return new MySqlDbSession(bag, dbconn, sp);
                }

                if (!(idbconn is null) && idbconn is MySqlConnection)
                {
                    return new MySqlDbSession(bag, idbconn, sp);
                }

                throw new InvalidOperationException($"The type of {nameof(MySqlConnection)} service is not registered.");
            });
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