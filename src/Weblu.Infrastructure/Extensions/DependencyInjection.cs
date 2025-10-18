using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Infrastructure.Localization;
using Weblu.Infrastructure.Repositories;
using Weblu.Infrastructure.Services;
using Weblu.Infrastructure.Token;

namespace Weblu.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IMethodRepository, MethodRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<TokenService>();






            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IErrorService, ErrorService>();

            services.AddScoped(typeof(IAppLogger<>), typeof(AppLoggerService<>));
        }
    }
}