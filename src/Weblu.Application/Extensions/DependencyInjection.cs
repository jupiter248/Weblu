using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Mappers;
using Weblu.Application.Services;

namespace Weblu.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IMethodService, MethodService>();
            services.AddScoped<IImageService, ImageService>();


            services.AddAutoMapper(typeof(ServiceProfile));
            services.AddAutoMapper(typeof(FeatureProfile));
            services.AddAutoMapper(typeof(MethodProfile));
            services.AddAutoMapper(typeof(ImageProfile));


        }
    }
}