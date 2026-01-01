using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Identity;
using Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Extensions
{
    public static class DatabaseConfigurations
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
        public static async Task ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                await SeedEntities.SeedRolesWithClaimsAsync(dbContext);
                await SeedEntities.SeedUserAndAdminAsync(dbContext);
            }
        }
    }
}