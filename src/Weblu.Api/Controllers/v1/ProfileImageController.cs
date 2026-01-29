using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ProfileDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/profile-image")]
    public class ProfileImageController : ControllerBase
    {
        private readonly IProfileImageService _profileImageService;
        public ProfileImageController(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }
        [Authorize(Policy = Permissions.ManageProfiles)]
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles([FromQuery] ProfileMediaParameters profileMediaParameters)
        {
            List<ProfileDto> profileDtos = await _profileImageService.GetAllProfilesAsync(profileMediaParameters);
            return Ok(profileDtos);
        }
        [Authorize(Policy = Permissions.ManageProfiles)]
        [HttpGet("{profileId:int}")]
        public async Task<IActionResult> GetProfileById(int profileId)
        {
            ProfileDto profileDto = await _profileImageService.GetProfileByIdAsync(profileId);
            return Ok(profileDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProfile([FromForm] AddProfileDto addProfileDto)
        {
            ProfileDto profileDto = await _profileImageService.AddProfileAsync(addProfileDto);
            return CreatedAtAction(nameof(GetProfileById), new { profileId = profileDto.Id }, ApiResponse<ProfileDto>.Success
            (
                "Profile image uploaded successfully.",
                profileDto
            ));
        }
        [Authorize]
        [HttpDelete("{profileId:int}")]
        public async Task<IActionResult> DeleteProfile(int profileId)
        {
            await _profileImageService.DeleteProfileAsync(profileId);
            return Ok(ApiResponse.Success
            (
                "Profile image successfully."
            ));
        }
    }
}