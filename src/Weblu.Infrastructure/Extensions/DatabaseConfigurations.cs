using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Extensions
{
    public static class DatabaseConfigurations
    {
        public static void ConnectToDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    string cs = Environment.GetEnvironmentVariable("DefaultConnection") ?? throw new Exception();
                    options.UseSqlServer(cs);
                }
            );
        }
        public static void ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}