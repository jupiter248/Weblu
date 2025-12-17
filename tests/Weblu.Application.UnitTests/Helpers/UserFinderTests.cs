using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Weblu.Application.Helpers;
using Xunit;

namespace Weblu.Application.UnitTests.Helpers
{
    public class UserFinderTests
    {
        [Fact]
        public void UserTests_GetUserId_ReturnUserId()
        {
            // Arrange 
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "user123")
            });
            
            var user = new ClaimsPrincipal(identity);

            // Act
            var act = user.GetUserId();

            // Assert
            act.Should().BeOfType<string?>();
            act.Should().BeLowerCased();
        }
    }
}