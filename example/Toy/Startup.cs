using System.Collections.Generic;
using System.Data;
using CorrelationId;
using KnstArchitecture.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using Serilog;
using Toy.Models.ExampleContextModels;

namespace Toy
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.WebHostEnvironment = webHostEnvironment;
            this.Configuration = configuration;

        }
        public IWebHostEnvironment WebHostEnvironment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IDbConnection>(sp => new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IDbConnection>(sp => new MySqlConnection(Configuration.GetConnectionString("SlaverConnection")));
            services.AddKnstArchitectureMultiSql();

            // 註冊 KnstArch.MySql 的框架
            services.AddKnstArchitectureMySqlWithQuery(Configuration.GetConnectionString("SlaverConnection"));
            // 註冊 MySql 的連線
            services.AddTransient<MySqlConnection>(sp => new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")));

            // 註冊 KnstArch.MongoDb 的框架
            services.AddKnstArchitectureMongoDb();
            // 註冊 MongoDb 的連線
            services.AddSingleton<IMongoClient>(new MongoClient(Configuration.GetConnectionString("MongoConnection")));

            services.AddHttpClient();
            services.AddCorrelationId();
            services.AddCorrelationIdHeaderPropagation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Toy", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorrelationId();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Toy V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseSerilogRequestLogging();

            app.UseHandleDbSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}