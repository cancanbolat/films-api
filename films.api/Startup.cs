using films.api.Services;
using films.api.Services.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            services.Configure<MongoDatabaseSettings>(Configuration.GetSection(nameof(MongoDatabaseSettings)));
            services.AddSingleton<IMongoDatabaseSettings>(options => options.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

            #region Life Cycles
            services.AddScoped<FilmService>();
            services.AddScoped<CastService>();
            #endregion

            services.AddControllers().AddNewtonsoftJson();

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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
