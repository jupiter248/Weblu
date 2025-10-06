using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Common.Interfaces;
using Weblu.Domain.Interfaces;
using Weblu.Infrastructure.Localization;
using Weblu.Infrastructure.Repositories;
using Weblu.Infrastructure.Services;

namespace Weblu.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();



            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IErrorService, ErrorService>();

            services.AddScoped(typeof(IAppLogger<>), typeof(AppLoggerService<>));
        }
    }
}