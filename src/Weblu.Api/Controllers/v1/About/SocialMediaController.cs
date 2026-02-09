using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.About.SocialMediaDTOs;
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
            List<SocialMediaDTO> socialMediaDTOs = await _socialMediaService.GetAllAsync(socialMediaParameters);
            return Ok(socialMediaDTOs);
        }
        [HttpGet("{socialMediaId:int}")]
        public async Task<IActionResult> GetById(int socialMediaId)
        {
            SocialMediaDTO socialMediaDTO = await _socialMediaService.GetByIdAsync(socialMediaId);
            return Ok(socialMediaDTO);
        }
        [Authorize(Policy = Permissions.ManageSocialMedia)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSocialMediaDTO createSocialMediaDTO)
        {
            Validator.ValidateAndThrow(createSocialMediaDTO, new CreateSocialMediaValidator());
            SocialMediaDTO socialMediaDTO = await _socialMediaService.CreateAsync(createSocialMediaDTO);
            return CreatedAtAction(nameof(GetById), new { socialMediaId = socialMediaDTO.Id }, ApiResponse<SocialMediaDTO>.Success("Social media added successfully", socialMediaDTO));
        }
        [Authorize(Policy = Permissions.ManageSocialMedia)]
        [HttpPut("{socialMediaId:int}")]
        public async Task<IActionResult> Update(int socialMediaId, [FromBody] UpdateSocialMediaDTO updateSocialMediaDTO)
        {
            Validator.ValidateAndThrow(updateSocialMediaDTO, new UpdateSocialMediaValidator());
            SocialMediaDTO socialMediaDTO = await _socialMediaService.UpdateAsync(socialMediaId, updateSocialMediaDTO);
            return Ok(ApiResponse<SocialMediaDTO>.Success(
                "Social media updated successfully",
                socialMediaDTO
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
        public async Task<IActionResult> UpdateIcon(int socialMediaId, [FromForm] ChangeSocialMediaIconDTO changeSocialMediaIconDTO)
        {
            Validator.ValidateAndThrow(changeSocialMediaIconDTO, new ChangeSocialMediaIconValidator());
            SocialMediaDTO socialMediaDTO = await _socialMediaService.ChangeIconAsync(socialMediaId, changeSocialMediaIconDTO);
            return Ok(ApiResponse<SocialMediaDTO>.Success(
                "Social media profile image updated",
                socialMediaDTO
            ));
        }
    }
}