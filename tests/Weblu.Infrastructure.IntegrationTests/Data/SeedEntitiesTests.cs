using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.IntegrationTests.Helpers;
using Xunit;

namespace Weblu.Infrastructure.IntegrationTests.Data
{
    public class SeedEntitiesTests : IClassFixture<SqlServerTestContainer>
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        public SeedEntitiesTests(SqlServerTestContainer container)
        {
            _options = container.Options;
        }
        [Fact]
        public async Task SeedEntities_SeedRolesWithClaimsAsync_InsertRolesWithClaimsDefault()
        {
            // Arrange 
            using var db = new ApplicationDbContext(_options);
            
            // Act
            await SeedEntities.SeedRolesWithClaimsAsync(db);

            // Assert
            db.Roles.Should().NotBeEmpty();
            db.Roles.Should().HaveCount(3);
            db.Roles.Should().Contain(u => u.NormalizedName == "ADMIN");
            db.Roles.Should().Contain(u => u.NormalizedName == "HEAD-ADMIN");
            db.Roles.Should().Contain(u => u.NormalizedName == "USER");


        }
        [Fact]
        public async Task SeedEntities_SeedUserAndAdminAsync_InsertUserAndAdminDefault()
        {
            // Arrange 
            using var db = new ApplicationDbContext(_options);

            // Act
            await SeedEntities.SeedUserAndAdminAsync(db);

            // Assert
            db.Users.Should().NotBeEmpty();
            db.UserRoles.Should().NotBeEmpty();
            db.Users.Should().HaveCount(2);
            db.Users.Should().Contain(u => u.NormalizedUserName == "USER");
            db.Users.Should().Contain(u => u.NormalizedUserName == "ADMIN");
        }
    }
}