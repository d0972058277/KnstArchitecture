using System;
using KnstArchitecture;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection TryAddLazy(this IServiceCollection services)
        {
            services.TryAddTransient(typeof(Lazy<>), typeof(Lazier<>));
            return services;
        }
    }
}