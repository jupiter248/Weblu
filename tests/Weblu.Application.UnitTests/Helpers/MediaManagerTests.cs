using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Weblu.Application.Dtos.MediaDtos;
using Weblu.Application.Helpers;
using Xunit;

namespace Weblu.Application.UnitTests.Helpers
{
    public class MediaManagerTests
    {
        private readonly IWebHostEnvironment _webHost;
        public MediaManagerTests()
        {
            _webHost = A.Fake<IWebHostEnvironment>();
        }
        [Fact]
        public void MediaManager_UploadMedia_ReturnMediaName()
        {
            // Arrange
            MediaUploaderDto mediaUploaderDto = new MediaUploaderDto
            {
                Media = A.Fake<IFormFile>(),
                MediaType = Weblu.Domain.Enums.Common.Media.MediaType.picture
            };
            // Act
            var act = MediaManager.UploadMedia(_webHost, mediaUploaderDto);

            // Assert
            act.Should().BeOfType<Task<string>>();
            act.Should().NotBeNull();
        }
        [Fact]
        public async Task MediaManager_DeleteMedia_ReturnTask()
        {
            // Arrange
            string mediaPath = "uploads/picture/sample.jpg";

            // Act
            Func<Task> act = () => MediaManager.DeleteMedia(_webHost, mediaPath);
    
            // Assert
            await act.Should().NotThrowAsync();
        }
    }
}