using films.api.Services;
using films.api.Services.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using films.api.Extensions;

namespace films.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Problems
            services.AddProblemDetails();

            //AutoMapper
            services.AddAutoMapper(typeof(Startup));

            var mongoDbSettings = Configuration.GetSection(nameof(MongoDatabaseSettings));
            services.Configure<MongoDatabaseSettings>(mongoDbSettings);

            services.ConfigureDependency(); //Dependency Extension

            services.ConfigureSwagger(); //Swagger Extension
            
            services.ConfigureCache(); //Cache Extension

            services.ConfigureCors(); //CORS Extension

            //Health Check
            services.AddHealthChecks().AddMongoDb(
                    Configuration["MongoDatabaseSettings:ConnectionString"],
                    name: "FilmsApi",
                    timeout: TimeSpan.FromSeconds(3),
                    tags: new[] { "ready" }
             );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Problems
            app.UseProblemDetails();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("_myAllowOrigins");
            app.UseResponseCaching();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCustomHealthChecks(); //HealthCheck Extension
        }
    }
}
