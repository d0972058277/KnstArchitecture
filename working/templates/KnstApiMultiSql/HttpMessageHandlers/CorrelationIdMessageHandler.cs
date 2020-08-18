using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CorrelationId
{
    public static class CorrelationIdExtension
    {
        public static IServiceCollection AddCorrelationIdHeaderPropagation(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpMessageHandlerBuilderFilter, CorrelationIdMessageHandlerBuilderFilter>();
            return services;
        }
    }

    public class CorrelationIdMessageHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
    {
        private readonly ICorrelationContextAccessor _correlationContextAccessor;

        public CorrelationIdMessageHandlerBuilderFilter(ICorrelationContextAccessor correlationContextAccessor)
        {
            _correlationContextAccessor = correlationContextAccessor;
        }

        public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            return builder =>
            {
                builder.AdditionalHandlers.Add(new CorrelationIdMessageHandler(_correlationContextAccessor));
                next(builder);
            };
        }
    }

    public class CorrelationIdMessageHandler : DelegatingHandler
    {
        private readonly ICorrelationContextAccessor _correlationContextAccessor;

        public CorrelationIdMessageHandler(ICorrelationContextAccessor correlationContextAccessor)
        {
            _correlationContextAccessor = correlationContextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (_correlationContextAccessor.CorrelationContext is null) return base.SendAsync(request, cancellationToken);
            if (!request.Headers.Contains(_correlationContextAccessor.CorrelationContext.Header))
            {
                request.Headers.Add(_correlationContextAccessor.CorrelationContext.Header, _correlationContextAccessor.CorrelationContext.CorrelationId);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
