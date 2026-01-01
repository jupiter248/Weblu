using DotNetEnv;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Weblu.Api.Controllers;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Extensions;

namespace Weblu.Api.IntegrationTests.Common
{
    internal class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        private string _connectionString { get; }
        public TestWebApplicationFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var contentRoot = context.HostingEnvironment.ContentRootPath;

                var envPath = Path.GetFullPath(
                    Path.Combine(contentRoot,  ".env.test")
                );
                if (File.Exists(envPath))
                {
                    Env.Load(envPath);
                }
            });
            builder.ConfigureTestServices(services =>
            {

                // Remove existing dbContext registration
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                // Register new dbContext
                services.AddDatabase(_connectionString);


                var provider = services.BuildServiceProvider();
                using var scope = provider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.EnsureDeleted();
            });
        }
    }

}