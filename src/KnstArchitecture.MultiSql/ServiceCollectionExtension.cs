using System.Data;
using System.Linq;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos.Factories;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddKnstArchitectureMultiSql(this IServiceCollection services)
        {
            services.TryAddKnstArchitecture();
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