using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace Weblu.Api.IntegrationTests.Helpers
{
    // This class creates an instance of a SQL Server test container for integration testing
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
        public string ConnectionString => $"{_container.GetConnectionString()};Database=Weblu_TestDb";

        public async ValueTask DisposeAsync()
        {
            await _container.DisposeAsync();
        }

        public async ValueTask InitializeAsync()
        {
            await _container.StartAsync();
        }
    }
}