using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace films.api.Extensions
{
    public static class ConfigureSwaggerExtension
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
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

            return services;
        }
    }
}
