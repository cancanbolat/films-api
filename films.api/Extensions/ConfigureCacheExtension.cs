using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Extensions
{
    public static class ConfigureCacheExtension
    {
        public static IServiceCollection ConfigureCache(this IServiceCollection services)
        {
            services.AddResponseCaching(); // response cache

            services.AddControllers(op =>
                op.CacheProfiles.Add("Duration20Cache", new CacheProfile { Duration = 20 })
            ).AddNewtonsoftJson();

            return services;
        }
    }
}
