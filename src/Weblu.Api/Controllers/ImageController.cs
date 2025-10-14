using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Dtos.ImageDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Parameters;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/image")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllImages([FromQuery] ImageParameters imageParameters)
        {
            List<ImageDto> imageDtos = await _imageService.GetAllImagesAsync(imageParameters);
            return Ok(imageDtos);
        }
        [HttpGet("{imageId:int}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            ImageDto imageDto = await _imageService.GetImageByIdAsync(imageId);
            return Ok(imageDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddImage([FromForm] AddImageDto addImageDto)
        {
            ImageDto imageDto = await _imageService.AddImageAsync(addImageDto);
            return CreatedAtAction(nameof(GetImageById), new { imageId = imageDto.Id }, new
            {
                message = "Image added successfully",
                image = imageDto
            });
        }
        [HttpDelete("{imageId:int}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            await _imageService.DeleteImageAsync(imageId);
            return Ok(new { message = "Image deleted successfully" });
        }
    }
}