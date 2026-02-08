using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.About.SocialMediaDtos;
using Weblu.Application.Interfaces.Services.About;
using Weblu.Application.Parameters.About;
using Weblu.Application.Validations;
using Weblu.Application.Validations.About.SocialMedias;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.AboutUs
{
    [ApiVersion("1")]
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
        public async Task<IActionResult> GetAll([FromQuery] SocialMediaParameters socialMediaParameters)
        {
            List<SocialMediaDto> socialMediaDtos = await _socialMediaService.GetAllAsync(socialMediaParameters);
            return Ok(socialMediaDtos);
        }
        [HttpGet("{socialMediaId:int}")]
        public async Task<IActionResult> GetById(int socialMediaId)
        {
            SocialMediaDto socialMediaDto = await _socialMediaService.GetByIdAsync(socialMediaId);
            return Ok(socialMediaDto);
        }
        [Authorize(Policy = Permissions.ManageSocialMedia)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSocialMediaDto createSocialMediaDto)
        {
            Validator.ValidateAndThrow(createSocialMediaDto, new CreateSocialMediaValidator());
            SocialMediaDto socialMediaDto = await _socialMediaService.CreateAsync(createSocialMediaDto);
            return CreatedAtAction(nameof(GetById), new { socialMediaId = socialMediaDto.Id }, ApiResponse<SocialMediaDto>.Success("Social media added successfully", socialMediaDto));
        }
        [Authorize(Policy = Permissions.ManageSocialMedia)]
        [HttpPut("{socialMediaId:int}")]
        public async Task<IActionResult> Update(int socialMediaId, [FromBody] UpdateSocialMediaDto updateSocialMediaDto)
        {
            Validator.ValidateAndThrow(updateSocialMediaDto, new UpdateSocialMediaValidator());
            SocialMediaDto socialMediaDto = await _socialMediaService.UpdateAsync(socialMediaId, updateSocialMediaDto);
            return Ok(ApiResponse<SocialMediaDto>.Success(
                "Social media updated successfully",
                socialMediaDto
            ));
        }
        [Authorize(Policy = Permissions.ManageSocialMedia)]
        [HttpDelete("{socialMediaId:int}")]
        public async Task<IActionResult> Delete(int socialMediaId)
        {
            await _socialMediaService.DeleteAsync(socialMediaId);
            return Ok(ApiResponse.Success(
                "Social media deleted successfully."
            ));
        }
        [Authorize(Policy = Permissions.ManageSocialMedia)]
        [HttpPut("{socialMediaId:int}/icon")]
        public async Task<IActionResult> UpdateIcon(int socialMediaId, [FromForm] ChangeSocialMediaIconDto changeSocialMediaIconDto)
        {
            Validator.ValidateAndThrow(changeSocialMediaIconDto, new ChangeSocialMediaIconValidator());
            SocialMediaDto socialMediaDto = await _socialMediaService.ChangeIconAsync(socialMediaId, changeSocialMediaIconDto);
            return Ok(ApiResponse<SocialMediaDto>.Success(
                "Social media profile image updated",
                socialMediaDto
            ));
        }
    }
}