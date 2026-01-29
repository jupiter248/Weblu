using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Weblu.Application.Exceptions;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Identity;
using Weblu.Infrastructure.Identity.Authorization;
using Weblu.Infrastructure.Identity.Entities;
using Weblu.Infrastructure.Token;

namespace Weblu.Infrastructure.Extensions
{
    public static class IdentityConfigurations
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyz0123456789._@";
                options.User.RequireUniqueEmail = false;

            }).AddRoles<IdentityRole>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
        public static void ConfigureJwt(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             })
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = Environment.GetEnvironmentVariable("JWT_Issuer"),
                     ValidAudience = Environment.GetEnvironmentVariable("JWT_Audience"),
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_Key") ?? throw new NotFoundException("Jwt key not found")))
                 };
             });
        }
        public static void ConfigureJwtSettings(this IServiceCollection services)
        {
            services.Configure<JwtSettings>(options =>
            {
                options.Key = Environment.GetEnvironmentVariable("JWT_Key") ?? throw new InvalidOperationException("JWT_Key missing");
                options.Audience = Environment.GetEnvironmentVariable("JWT_Audience") ?? throw new InvalidOperationException("JWT_Audience missing");
                options.Issuer = Environment.GetEnvironmentVariable("JWT_Issuer") ?? throw new InvalidOperationException("JWT_Issuer missing");
                options.ExpiryMinutes = Int32.Parse(Environment.GetEnvironmentVariable("JWT_ExpiryMinutes") ?? throw new InvalidOperationException("JWT_ExpiryMinutes missing"));
            });
        }
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                foreach (var permission in Permissions.All)
                {
                    options.AddPolicy(permission, policy => policy.RequireClaim(CustomClaimTypes.Permission, permission));
                }
            });
        }
    }
}