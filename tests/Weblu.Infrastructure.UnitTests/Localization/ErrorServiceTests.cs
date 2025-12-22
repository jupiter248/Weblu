using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Weblu.Infrastructure.Localization;
using Xunit;

namespace Weblu.Infrastructure.UnitTests.Localization
{
    public class ErrorServiceTests
    {
        private readonly ErrorService _errorService;
        public ErrorServiceTests()
        {
            _errorService = new ErrorService();
        }
        [Fact]
        public void ErrorService_GetMessage_ReturnEnglishErrorMessage()
        {
            // Arrange
            // default: CultureInfo is english 
            string errorCode = "TEST_NOT_FOUND";
            string errorMessage = "Test not found.";

            // Act 
            var act = _errorService.GetMessage(errorCode);

            // Assert
            act.Should().NotBeNullOrWhiteSpace();
            act.Should().Be(errorMessage);
            act.Should().BeOfType<string>();

        }
        [Fact]
        public void ErrorService_GetMessage_ReturnFarsiErrorMessage()
        {
            // Arrange
            CultureInfo cultureInfo = new CultureInfo("fa-IR");
            CultureInfo.CurrentUICulture = cultureInfo;

            string errorCode = "TEST_NOT_FOUND";
            string errorMessage = ".تست پیدا نشد";

            // Act 
            var act = _errorService.GetMessage(errorCode);

            // Assert
            act.Should().NotBeNullOrWhiteSpace();
            act.Should().Be(errorMessage);
            act.Should().BeOfType<string>();

        }
    }
}