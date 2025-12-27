using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.IntegrationTests.Helpers;

namespace Weblu.Infrastructure.IntegrationTests.Data
{
    public class MigrationsTests : IClassFixture<SqlServerTestContainer>
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        public MigrationsTests(SqlServerTestContainer container)
        {
            _options = container.Options;
        }

        [Fact]
        public void Database_Should_Have_No_Pending_Migrations()
        {
            // Arrange
            using var db = new ApplicationDbContext(_options);

            // Act
            var pending = db.Database.GetPendingMigrations();

            // Assert
            pending.Should().BeEmpty();
        }
    }
}