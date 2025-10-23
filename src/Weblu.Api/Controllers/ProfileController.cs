using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ProfileDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/profile-image")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileUserService _profileUserService;
        public ProfileController(IProfileUserService profileUserService)
        {
            _profileUserService = profileUserService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles([FromQuery] ProfileMediaParameters profileMediaParameters)
        {
            List<ProfileDto> profileDtos = await _profileUserService.GetAllProfilesAsync(profileMediaParameters);
            return Ok(profileDtos);
        }
        [HttpGet("{profileId:int}")]
        public async Task<IActionResult> GetProfileById(int profileId)
        {
            ProfileDto profileDto = await _profileUserService.GetProfileByIdAsync(profileId);
            return Ok(profileDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddProfile([FromForm] AddProfileDto addProfileDto)
        {
            ProfileDto profileDto = await _profileUserService.AddProfileAsync(addProfileDto);
            return CreatedAtAction(nameof(GetProfileById), new { profileId = profileDto.Id }, ApiResponse<ProfileDto>.Success
            (
                "Profile image uploaded successfully.",
                profileDto
            ));
        }
        [HttpDelete("{profileId:int}")]
        public async Task<IActionResult> DeleteProfile(int profileId)
        {
            await _profileUserService.DeleteProfileAsync(profileId);
            return Ok(ApiResponse.Success
            (
                "Profile image successfully."
            ));
        }
    }
}