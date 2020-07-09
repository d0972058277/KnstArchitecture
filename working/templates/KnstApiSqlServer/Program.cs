using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;

namespace KnstApiSqlServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional : true, reloadOnChange : true)
                .AddEnvironmentVariables();
            IConfiguration configuration = configurationBuilder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithCorrelationIdHeader()
                .Enrich.WithProperty("ApplicationName", AppDomain.CurrentDomain.FriendlyName)
                .Enrich.WithProperty("EnvironmentName", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                .CreateLogger();

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppDomain.CurrentDomain.FriendlyName);
                IHost host = CreateHostBuilder(args).Build();

                Log.Information("Starting web host ({ApplicationContext})...", AppDomain.CurrentDomain.FriendlyName);
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppDomain.CurrentDomain.FriendlyName);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog();
    }
}