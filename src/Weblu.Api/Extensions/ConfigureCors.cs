using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Api.Extensions
{
    public static class ConfigureCors
    {
        public static void ApplyCors(this IServiceCollection services)
        {
            services.AddCors(opt =>
            opt.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin();
            }));
        }
    }
}