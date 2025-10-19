using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weblu.Api.Extensions
{
    public static class ConfigureControllers
    {
        public static void AddControllersConfigurations(this IServiceCollection services)
        {
            services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    // This disables the default automatic 400 response
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
        }
    }
}