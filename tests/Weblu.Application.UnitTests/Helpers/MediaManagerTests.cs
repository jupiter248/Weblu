using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Dtos.Images.MediaDtos;
using Weblu.Application.Helpers;
using Xunit;

namespace Weblu.Application.UnitTests.Helpers
{
    public class MediaManagerTests
    {
        private readonly IFilePathProviderService _webHost;
        private readonly string _webHostPath;
        public MediaManagerTests()
        {
            _webHost = A.Fake<IFilePathProviderService>();
            _webHostPath = _webHost.GetWebRootPath();
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
            var act = MediaManager.UploadMedia(_webHostPath, mediaUploaderDto);

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
            Func<Task> act = () => MediaManager.DeleteMedia(_webHostPath, mediaPath);
    
            // Assert
            await act.Should().NotThrowAsync();
        }
    }
}