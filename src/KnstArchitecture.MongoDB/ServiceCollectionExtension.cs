using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddKnstArchitectureMongoDb(this IServiceCollection services)
        {
            services.TryAddKnstArchitecture();
            services.AddTransient<IMongoDbSession, MongoDbSession>();
            services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();
            return services;
        }
    }
}