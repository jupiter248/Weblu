using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Weblu.Application.Helpers;

namespace Weblu.Application.UnitTests.Helpers
{
    public class DateConvertorTests
    {
        [Fact]
        public void DateConvertor_ToShamsi_ReturnShamsiDateInString()
        {
            // Arrange
            DateTimeOffset dateTimeOffset = new DateTimeOffset(2024, 6, 15, 14, 30, 0, TimeSpan.Zero);

            // Act
            string act = dateTimeOffset.ToShamsi();

            // Assert
            act.Should().BeOfType<string>();
            act.Should().NotBeNullOrWhiteSpace();
        }
    }
}