using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.IntegrationTests.Helpers;

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
            db.Roles.Should().HaveCount(4);
            db.Roles.Should().Contain(u => u.NormalizedName == "ADMIN");
            db.Roles.Should().Contain(u => u.NormalizedName == "HEAD-ADMIN");
            db.Roles.Should().Contain(u => u.NormalizedName == "USER");
            db.Roles.Should().Contain(u => u.NormalizedName == "EDITOR");



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
            db.Users.Should().HaveCount(4);
            db.Users.Should().Contain(u => u.NormalizedUserName == "USER");
            db.Users.Should().Contain(u => u.NormalizedUserName == "ADMIN");
        }
    }
}