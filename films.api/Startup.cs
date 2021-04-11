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

namespace films.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowOrigins = "_myAllowOrigins";
        public void ConfigureServices(IServiceCollection services)
        {
            // Problems
            services.AddProblemDetails();

            //AutoMapper
            services.AddAutoMapper(typeof(Startup));

            services.Configure<MongoDatabaseSettings>(Configuration.GetSection(nameof(MongoDatabaseSettings)));
            services.AddSingleton<IMongoDatabaseSettings>(options => options.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

            #region Swagger
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Films API",
                    Description = "ASP Net Core Films API. Mongo DB and Generic Repository.",
                    TermsOfService = new Uri("https://www.cancanbolat.site"),
                    Contact = new OpenApiContact
                    {
                        Name = "Can Canbolat",
                        Email = "canxmcclyn@gmail.com"
                    }
                });

                // XML
                var xmlFile = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
            #endregion

            #region Life Cycles
            services.AddScoped<FilmService>();
            services.AddScoped<CastService>();
            #endregion

            services.AddResponseCaching(); // response cache

            services.AddControllers(op =>
                op.CacheProfiles.Add("Duration20Cache", new CacheProfile { Duration = 20 })
            ).AddNewtonsoftJson();

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: MyAllowOrigins,
                    builder =>
                    {
                        builder
                               .AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    }
                    );
            });
            #endregion
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

            app.UseCors(MyAllowOrigins); //cors
            app.UseResponseCaching(); //cache

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI();
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
