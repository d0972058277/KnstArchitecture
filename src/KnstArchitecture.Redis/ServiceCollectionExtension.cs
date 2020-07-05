using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddKnstArchitectureRedis(this IServiceCollection services)
        {
            services.TryAddKnstArchitecture();
            services.AddTransient<IRedisDbSession, RedisDbSession>();
            services.AddScoped<IRedisUnitOfWork, RedisUnitOfWork>();
            return services;
        }
    }
}