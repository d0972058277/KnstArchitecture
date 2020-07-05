using System;
using System.Linq;
using KnstArchitecture.EF.DbContexts;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection TryAddKnstDbContexts(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(t => t.IsSubclassOf(typeof(KnstDbContext)) && !t.IsAbstract).ToList();
            foreach (var t in types)
            {
                services.TryAddScoped(t);
            }
            return services;
        }
    }
}