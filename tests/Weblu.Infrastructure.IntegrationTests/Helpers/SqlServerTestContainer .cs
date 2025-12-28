using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using Weblu.Infrastructure.Data;
using Xunit;

namespace Weblu.Infrastructure.IntegrationTests.Helpers
{
    public class SqlServerTestContainer : IAsyncLifetime
    {
        private readonly MsSqlContainer _container;

        public SqlServerTestContainer()
        {
            _container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("YourStrong!Passw0rd")
                .WithCleanUp(true)
                .Build();
        }

        public DbContextOptions<ApplicationDbContext> Options { get; private set; } = default!;

        public async Task InitializeAsync()
        {
            await _container.StartAsync();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_container.GetConnectionString())
                .Options;

            using var context = new ApplicationDbContext(Options);
            await context.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            await _container.DisposeAsync();
        }
    }
}