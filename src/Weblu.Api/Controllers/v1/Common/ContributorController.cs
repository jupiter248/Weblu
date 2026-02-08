using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Common.ContributorDtos;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Common.Contributors;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Common
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/contributor")]
    public class ContributorController : ControllerBase
    {
        private readonly IContributorService _contributorService;
        public ContributorController(IContributorService contributorService)
        {
            _contributorService = contributorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ContributorParameters contributorParameters)
        {
            List<ContributorDto> contributorDtos = await _contributorService.GetAllAsync(contributorParameters);
            return Ok(contributorDtos);
        }
        [HttpGet("{contributorId:int}")]
        public async Task<IActionResult> GetById(int contributorId)
        {
            ContributorDto contributorDto = await _contributorService.GetByIdAsync(contributorId);
            return Ok(contributorDto);
        }
        [Authorize(Policy = Permissions.ManageContributors)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContributorDto createContributorDto)
        {
            Validator.ValidateAndThrow(createContributorDto, new CreateContributorValidator());
            ContributorDto contributorDto = await _contributorService.CreateAsync(createContributorDto);
            return CreatedAtAction(nameof(GetById), new { contributorId = contributorDto.Id }, ApiResponse<ContributorDto>.Success("Contributor added successfully", contributorDto));
        }
        [Authorize(Policy = Permissions.ManageContributors)]
        [HttpPut("{contributorId:int}")]
        public async Task<IActionResult> Update(int contributorId, [FromBody] UpdateContributorDto updateContributorDto)
        {
            Validator.ValidateAndThrow(updateContributorDto, new UpdateContributorValidator());
            ContributorDto contributorDto = await _contributorService.UpdateAsync(contributorId, updateContributorDto);
            return Ok(ApiResponse<ContributorDto>.Success(
                "Contributor updated successfully",
                contributorDto
            ));
        }
        [Authorize(Policy = Permissions.ManageContributors)]
        [HttpDelete("{contributorId:int}")]
        public async Task<IActionResult> Delete(int contributorId)
        {
            await _contributorService.DeleteAsync(contributorId);
            return Ok(ApiResponse.Success(
                "Contributor deleted successfully."
            ));
        }
        [Authorize(Policy = Permissions.ManageContributors)]
        [HttpPut("{contributorId:int}/profile-image")]
        public async Task<IActionResult> ChangeImageProfile(int contributorId, [FromForm] ChangeContributorProfileImageDto changeContributorProfileImageDto)
        {
            Validator.ValidateAndThrow(changeContributorProfileImageDto, new ChangeContributorProfileImageValidator());
            ContributorDto contributorDto = await _contributorService.ChangeProfileImageAsync(contributorId, changeContributorProfileImageDto);
            return Ok(ApiResponse<ContributorDto>.Success(
                "Contributor profile image updated successfully.",
                contributorDto
            ));
        }
        [Authorize(Policy = Permissions.ManageContributors)]
        [HttpDelete("{contributorId:int}/profile-image")]
        public async Task<IActionResult> DeleteImageProfile(int contributorId)
        {
            await _contributorService.DeleteProfileAsync(contributorId);
            return Ok(ApiResponse.Success(
                "Contributor Profile image deleted successfully."
            ));
        }
    }
}