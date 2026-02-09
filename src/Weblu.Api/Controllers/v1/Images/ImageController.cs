using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Images.ImageDTOs;
using Weblu.Application.Interfaces.Services.Images;
using Weblu.Application.Parameters.Images;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Images
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
        public async Task<IActionResult> GetAll([FromQuery] ImageParameters imageParameters)
        {
            List<ImageDTO> imageDTOs = await _imageService.GetAllAsync(imageParameters);
            return Ok(imageDTOs);
        }
        [Authorize(Policy = Permissions.ManageImages)]
        [HttpGet("{imageId:int}")]
        public async Task<IActionResult> GetById(int imageId)
        {
            ImageDTO imageDTO = await _imageService.GetByIdAsync(imageId);
            return Ok(imageDTO);
        }
        [Authorize(Policy = Permissions.ManageImages)]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddImageDTO addImageDTO)
        {
            ImageDTO imageDTO = await _imageService.AddAsync(addImageDTO);
            return CreatedAtAction(nameof(GetById), new { imageId = imageDTO.Id }, ApiResponse<ImageDTO>.Success
            (
                "Image uploaded successfully.",
                imageDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageImages)]
        [HttpDelete("{imageId:int}")]
        public async Task<IActionResult> Delete(int imageId)
        {
            await _imageService.DeleteAsync(imageId);
            return Ok(ApiResponse.Success
            (
                "Image deleted successfully."
            ));
        }
    }
}