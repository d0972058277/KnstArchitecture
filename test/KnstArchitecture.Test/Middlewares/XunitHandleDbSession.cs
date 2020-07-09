using System;
using System.Threading.Tasks;
using KnstArchitecture.Base.Test;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace KnstArchitecture.Test.Middlewares
{
    public class XunitHandleDbSession : XunitKnstArch
    {
        [Fact]
        public async Task HandleDbSessionCommit()
        {
            ITestDbSession session = null;
            using var host = await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {
                            services.TryAddTransient<ITestRepo, TestRepo>();
                            services.TryAddKnstArchitectureDbSessionBag();
                            services.AddTransient<ITestDbSession, TestDbSession>();
                            services.AddScoped<ITestUnitOfWork, TestUnitOfWork>();
                        })
                        .Configure(app =>
                        {
                            app.UseMiddleware<HandleDbSessionMiddleware>();
                            app.Use(async(context, next) =>
                            {
                                var sp = context.RequestServices;
                                var bag = sp.GetRequiredService<IDbSessionBag>();
                                session = sp.GetRequiredService<ITestDbSession>();
                                session.BeginTransaction();
                                await next();
                            });
                        });
                })
                .StartAsync();

            var response = await host.GetTestServer().CreateClient().GetAsync("/");

            Assert.False(session.IsTransaction);
        }

        [Fact]
        public async Task HandleDbSessionRollback()
        {
            ITestDbSession session = null;
            using var host = await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {
                            services.TryAddTransient<ITestRepo, TestRepo>();
                            services.TryAddKnstArchitectureDbSessionBag();
                            services.AddTransient<ITestDbSession, TestDbSession>();
                            services.AddScoped<ITestUnitOfWork, TestUnitOfWork>();
                        })
                        .Configure(app =>
                        {
                            app.UseMiddleware<HandleDbSessionMiddleware>();
                            app.Use(async(context, next) =>
                            {
                                var sp = context.RequestServices;
                                var bag = sp.GetRequiredService<IDbSessionBag>();
                                session = sp.GetRequiredService<ITestDbSession>();
                                session.BeginTransaction();
                                await Task.CompletedTask;
                                throw new Exception();
                            });
                        });
                })
                .StartAsync();

            await Assert.ThrowsAsync<Exception>(async() => await host.GetTestServer().CreateClient().GetAsync("/"));

            Assert.False(session.IsTransaction);
        }
    }
}