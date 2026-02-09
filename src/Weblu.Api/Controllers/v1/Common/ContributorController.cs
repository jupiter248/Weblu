using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Common.ContributorDTOs;
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
            List<ContributorDTO> contributorDTOs = await _contributorService.GetAllAsync(contributorParameters);
            return Ok(contributorDTOs);
        }
        [HttpGet("{contributorId:int}")]
        public async Task<IActionResult> GetById(int contributorId)
        {
            ContributorDTO contributorDTO = await _contributorService.GetByIdAsync(contributorId);
            return Ok(contributorDTO);
        }
        [Authorize(Policy = Permissions.ManageContributors)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContributorDTO createContributorDTO)
        {
            Validator.ValidateAndThrow(createContributorDTO, new CreateContributorValidator());
            ContributorDTO contributorDTO = await _contributorService.CreateAsync(createContributorDTO);
            return CreatedAtAction(nameof(GetById), new { contributorId = contributorDTO.Id }, ApiResponse<ContributorDTO>.Success("Contributor added successfully", contributorDTO));
        }
        [Authorize(Policy = Permissions.ManageContributors)]
        [HttpPut("{contributorId:int}")]
        public async Task<IActionResult> Update(int contributorId, [FromBody] UpdateContributorDTO updateContributorDTO)
        {
            Validator.ValidateAndThrow(updateContributorDTO, new UpdateContributorValidator());
            ContributorDTO contributorDTO = await _contributorService.UpdateAsync(contributorId, updateContributorDTO);
            return Ok(ApiResponse<ContributorDTO>.Success(
                "Contributor updated successfully",
                contributorDTO
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
        [HttpPut("{articleId:int}/publish")]
        public async Task<IActionResult> Publish(int articleId)
        {
            await _contributorService.Publish(articleId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManageContributors)]
        [HttpPut("{articleId:int}/unpublish")]
        public async Task<IActionResult> Unpublish(int articleId)
        {
            await _contributorService.Unpublish(articleId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManageContributors)]
        [HttpPut("{contributorId:int}/profile-image")]
        public async Task<IActionResult> ChangeImageProfile(int contributorId, [FromForm] ChangeContributorProfileImageDTO changeContributorProfileImageDTO)
        {
            Validator.ValidateAndThrow(changeContributorProfileImageDTO, new ChangeContributorProfileImageValidator());
            ContributorDTO contributorDTO = await _contributorService.ChangeProfileImageAsync(contributorId, changeContributorProfileImageDTO);
            return Ok(ApiResponse<ContributorDTO>.Success(
                "Contributor profile image updated successfully.",
                contributorDTO
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