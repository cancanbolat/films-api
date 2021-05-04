using AutoMapper.Configuration;
using films.api.Services;
using films.api.Services.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Extensions
{
    public static class ConfigureDependencyExtension
    {
        public static IServiceCollection ConfigureDependency(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDatabaseSettings>(options => options.
                GetRequiredService<IOptions<MongoDatabaseSettings>>().Value
            );

            services.AddScoped<FilmService>();
            services.AddScoped<CastService>();

            return services;
        }
    }
}
