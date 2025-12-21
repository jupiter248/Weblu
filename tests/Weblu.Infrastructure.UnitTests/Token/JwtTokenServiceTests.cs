using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Weblu.Application.Exceptions;
using Weblu.Infrastructure.Identity.Entities;
using Weblu.Infrastructure.Token;
using Xunit;

namespace Weblu.Infrastructure.UnitTests.Token
{
    public class JwtTokenServiceTests
    {
        public readonly AppUser _user;
        public readonly List<string> _roles;
        public JwtTokenServiceTests()
        {
            _user = new AppUser()
            {
                FirstName = "Mohammad",
                LastName = "Azimifar",
                PhoneNumber = "989031883414",
                UserName = "TestUsername",
            };
            _roles = new List<string> { "User" };

        }
        public static JwtTokenService CreateSut()
        {
            var settings = Options.Create(new JwtSettings
            {
                Key = "THIS_IS_A_SUPER_SECRET_KEY_123456789",
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                ExpiryMinutes = 30,
            });

            return new JwtTokenService(settings);
        }
        [Fact]
        public void JwtTokenService_GenerateAccessToken_ReturnJwtToken()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var act = sut.GenerateAccessToken(_user, _roles);

            // Assert
            act.Should().NotBeNullOrWhiteSpace();
            act.Split('.').Length.Should().Be(3); // JWT format
        }
        [Fact]
        public void JwtTokenService_GenerateAccessToken_ReturnExpectedClaims()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var token = sut.GenerateAccessToken(_user, _roles);
            var handler = new JwtSecurityTokenHandler();
            var act = handler.ReadJwtToken(token);

            // Assert
            act.Claims.Should().Contain(c =>
                c.Type == JwtRegisteredClaimNames.NameId &&
                c.Value == _user.Id.ToString());
            act.Claims.Should().Contain(c => c.Type == "role" && c.Value == _roles.First());
        }
        [Fact]
        public void JwtTokenService_GenerateAccessToken_ReturnExpectedExpiryMinutes()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var token = sut.GenerateAccessToken(_user, _roles);
            var handler = new JwtSecurityTokenHandler();
            var act = handler.ReadJwtToken(token);

            // Assert
            act.ValidTo.Should().BeAfter(DateTime.UtcNow);
            act.ValidTo.Should().NotBeAfter(DateTime.UtcNow.AddMinutes(30));
        }
        [Fact]
        public void JwtTokenService_GenerateRefreshToken_ReturnRandomString()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var act = sut.GenerateRefreshToken();

            // Assert 
            act.Should().NotBeNullOrWhiteSpace();
        }
    }
}