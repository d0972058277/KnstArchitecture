using System.Threading.Tasks;
using KnstArchitecture.DbSessions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KnstArchitecture.Middlewares
{
    public class HandleDbSessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandleDbSessionMiddleware> _logger;

        public HandleDbSessionMiddleware(RequestDelegate next, ILogger<HandleDbSessionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public virtual async Task Invoke(HttpContext context)
        {
            IDbSessionBag dbSessionBag = null;
            try
            {
                dbSessionBag = context.RequestServices.GetRequiredService<IDbSessionBag>();
                await _next(context);
                if (dbSessionBag.Empty) return;
                dbSessionBag?.Commit();
            }
            catch
            {
                dbSessionBag?.Rollback();
                throw;
            }
        }
    }

}