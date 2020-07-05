using KnstArchitecture.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHandleDbSession(this IApplicationBuilder app)
        {
            app.UseMiddleware<HandleDbSessionMiddleware>();
            return app;
        }
    }
}