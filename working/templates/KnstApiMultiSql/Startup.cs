using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CorrelationId;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using Serilog;

namespace KnstApiMultiSql
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 註冊 KnstArch.MultiSql 的框架
            services.AddKnstArchitectureMultiSql();
            // 註冊不同 IDbConnection 的連線
            services.AddTransient<IDbConnection>(sp => new MySqlConnection(Configuration.GetConnectionString("FirstConnection")));
            services.AddTransient<IDbConnection>(sp => new SqlConnection(Configuration.GetConnectionString("SecondConnection")));
            services.AddTransient<IDbConnection>(sp => new SqliteConnection(Configuration.GetConnectionString("ThirdConnection")));

            // 註冊所有實作 IService 的類別
            services.TryAddKnstArchitectureServices(ServiceLifetime.Transient);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{AppDomain.CurrentDomain.FriendlyName}-{WebHostEnvironment.EnvironmentName}", Version = "v1" });

                var dir = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory));
                foreach (var fi in dir.EnumerateFiles("*.xml"))
                {
                    c.IncludeXmlComments(fi.FullName);
                }
            });

            services.AddHttpClient();
            services.AddCorrelationId();
            services.AddCorrelationIdHeaderPropagation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/Error");

            app.UseCorrelationId();

            app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
            app.UseStaticFiles();

            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppDomain.CurrentDomain.FriendlyName}-{env.EnvironmentName}");
                    // c.RoutePrefix = string.Empty;
                    c.EnableDeepLinking();
                });
            }

            app.UseSerilogRequestLogging();

            app.UseHandleDbSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}