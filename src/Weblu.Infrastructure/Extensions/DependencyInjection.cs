using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Infrastructure.Identity.Mappers;
using Weblu.Infrastructure.Identity.Services;
using Weblu.Infrastructure.Localization;
using Weblu.Infrastructure.Logger;
using Weblu.Infrastructure.Repositories;
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
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();



            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IErrorService, ErrorService>();
            services.AddScoped<IUserService, UserService>();


            services.AddScoped(typeof(IAppLogger<>), typeof(AppLoggerService<>));
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}