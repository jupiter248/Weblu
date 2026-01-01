using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Routing.Patterns;

namespace Weblu.Api.Extensions
{
    public static class VersioningConfigurations
    {
        public static void ApplyVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;

            });

        }
    }
}