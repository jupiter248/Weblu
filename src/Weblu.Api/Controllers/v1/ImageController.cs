using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ImageDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;

namespace Weblu.Api.Controllers
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllImages([FromQuery] ImageParameters imageParameters)
        {
            List<ImageDto> imageDtos = await _imageService.GetAllImagesAsync(imageParameters);
            return Ok(imageDtos);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{imageId:int}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            ImageDto imageDto = await _imageService.GetImageByIdAsync(imageId);
            return Ok(imageDto);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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