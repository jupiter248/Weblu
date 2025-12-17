using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Weblu.Application.Helpers;
using Xunit;

namespace Weblu.Application.UnitTests.Helpers
{
    public class UrlValidatorTests
    {
        [Fact]
        public void UrlValidator_BeAValidUrl_ReturnTrue()
        {
            // Arrange
            string url = "https://www.example.com";

            // Act
            bool act = UrlValidator.BeAValidUrl(url);

            // Assert
            act.Should().BeTrue();

        }
    }
}