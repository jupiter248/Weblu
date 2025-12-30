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
                options.DefaultApiVersion = new ApiVersion(1);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader("x-api-version"));
                    new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
                opt.DefaultApiVersion = new ApiVersion(1);
                opt.AssumeDefaultVersionWhenUnspecified = true;
            });

        }
    }
}