using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Api.Extensions;
using Weblu.Api.Models;
using Weblu.Application.Common.Models;
using Weblu.Application.DTOs.Images.ProfileDTOs;
using Weblu.Application.Interfaces.Services.Images;
using Weblu.Application.Parameters.Images;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Images;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Images
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
        public async Task<IActionResult> GetAll([FromQuery] ProfileMediaParameters profileMediaParameters)
        {
            List<ProfileDTO> profileDTOs = await _profileImageService.GetAllAsync(profileMediaParameters);
            return Ok(profileDTOs);
        }
        [Authorize(Policy = Permissions.ManageProfiles)]
        [HttpGet("{profileId:int}")]
        public async Task<IActionResult> GetById(int profileId)
        {
            ProfileDTO profileDTO = await _profileImageService.GetByIdAsync(profileId);
            return Ok(profileDTO);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddProfileDTO addProfileDTO, AddImageRequest imageRequest)
        {
            Validator.ValidateAndThrow(imageRequest.ToAddImageDTO(), new UploadImageValidator());
            ProfileDTO profileDTO = await _profileImageService.AddAsync(addProfileDTO, imageRequest.ToAddImageDTO());
            return CreatedAtAction(nameof(GetById), new { profileId = profileDTO.Id }, ApiResponse<ProfileDTO>.Success
            (
                "Profile image uploaded successfully.",
                profileDTO
            ));
        }
        [Authorize]
        [HttpDelete("{profileId:int}")]
        public async Task<IActionResult> Delete(int profileId)
        {
            await _profileImageService.DeleteAsync(profileId);
            return Ok(ApiResponse.Success
            (
                "Profile image successfully."
            ));
        }
    }
}