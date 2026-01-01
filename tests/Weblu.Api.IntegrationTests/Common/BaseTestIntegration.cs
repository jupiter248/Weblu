using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Api.IntegrationTests.Helpers;
using Weblu.Infrastructure.Data;

namespace Weblu.Api.IntegrationTests.Common
{
    public class BaseTestIntegration : IClassFixture<SqlServerTestContainer>
    {
        protected readonly ApplicationDbContext _db;
        protected readonly HttpClient _client;
        public BaseTestIntegration(SqlServerTestContainer container)
        {
            var factory = new TestWebApplicationFactory(container.ConnectionString);

            _client = factory.CreateClient();


            IServiceScope scope = factory.Services.CreateScope();
            _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }
    }
}