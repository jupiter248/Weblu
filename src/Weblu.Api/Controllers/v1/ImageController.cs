using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ImageDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/image")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [Authorize(Policy = Permissions.ManageImages)]
        [HttpGet]
        public async Task<IActionResult> GetAllImages([FromQuery] ImageParameters imageParameters)
        {
            List<ImageDto> imageDtos = await _imageService.GetAllImagesAsync(imageParameters);
            return Ok(imageDtos);
        }
        [Authorize(Policy = Permissions.ManageImages)]
        [HttpGet("{imageId:int}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            ImageDto imageDto = await _imageService.GetImageByIdAsync(imageId);
            return Ok(imageDto);
        }
        [Authorize(Policy = Permissions.ManageImages)]
        [HttpPost]
        public async Task<IActionResult> AddImage([FromForm] AddImageDto addImageDto)
        {
            ImageDto imageDto = await _imageService.AddImageAsync(addImageDto);
            return CreatedAtAction(nameof(GetImageById), new { imageId = imageDto.Id }, ApiResponse<ImageDto>.Success
            (
                "Image uploaded successfully.",
                imageDto
            ));
        }
        [Authorize(Policy = Permissions.ManageImages)]
        [HttpDelete("{imageId:int}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            await _imageService.DeleteImageAsync(imageId);
            return Ok(ApiResponse.Success
            (
                "Image deleted successfully."
            ));
        }
    }
}