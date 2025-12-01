using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.SocialMediaDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Validations;
using Weblu.Application.Validations.SocialMedias;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/social-media")]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaService _socialMediaService;
        public SocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSocialMedia()
        {
            List<SocialMediaDto> socialMediaDtos = await _socialMediaService.GetAllSocialMediasAsync();
            return Ok(socialMediaDtos);
        }
        [HttpGet("{socialMediaId:int}")]
        public async Task<IActionResult> GetSocialMediaById(int socialMediaId)
        {
            SocialMediaDto socialMediaDto = await _socialMediaService.GetSocialMediaByIdAsync(socialMediaId);
            return Ok(socialMediaDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddSocialMedia([FromBody] AddSocialMediaDto addSocialMediaDto)
        {
            Validator.ValidateAndThrow(addSocialMediaDto, new AddSocialMediaValidator());
            SocialMediaDto socialMediaDto = await _socialMediaService.AddSocialMediaAsync(addSocialMediaDto);
            return CreatedAtAction(nameof(GetSocialMediaById), new { socialMediaId = socialMediaDto.Id }, ApiResponse<SocialMediaDto>.Success("Social media added successfully", socialMediaDto));
        }
        [HttpPut("{socialMediaId:int}")]
        public async Task<IActionResult> UpdateSocialMedia(int socialMediaId, [FromBody] UpdateSocialMediaDto updateSocialMediaDto)
        {
            Validator.ValidateAndThrow(updateSocialMediaDto, new UpdateSocialMediaValidator());
            SocialMediaDto socialMediaDto = await _socialMediaService.UpdateSocialMediaAsync(socialMediaId, updateSocialMediaDto);
            return Ok(ApiResponse<SocialMediaDto>.Success(
                "Social media updated successfully",
                socialMediaDto
            ));
        }
        [HttpDelete("{socialMediaId:int}")]
        public async Task<IActionResult> DeleteSocialMedia(int socialMediaId)
        {
            await _socialMediaService.DeleteSocialMediaAsync(socialMediaId);
            return Ok(ApiResponse.Success(
                "Social media deleted successfully."
            ));
        }
        [HttpPut("{socialMediaId:int}/icon")]
        public async Task<IActionResult> UpdateSocialMediaIcon(int socialMediaId, [FromForm] UpdateSocialMediaIconDto updateSocialMediaIconDto)
        {
            Validator.ValidateAndThrow(updateSocialMediaIconDto, new UpdateSocialMediaIconValidator());
            SocialMediaDto socialMediaDto = await _socialMediaService.UpdateSocialMediaIconAsync(socialMediaId, updateSocialMediaIconDto);
            return Ok(ApiResponse<SocialMediaDto>.Success(
                "Social media profile image updated",
                socialMediaDto
            ));
        }
    }
}