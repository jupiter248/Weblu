using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ContributorDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Contributors;

namespace Weblu.Api.Controllers
{
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
        public async Task<IActionResult> GetAllContributors([FromQuery] ContributorParameters contributorParameters)
        {
            List<ContributorDto> contributorDtos = await _contributorService.GetAllContributorsAsync(contributorParameters);
            return Ok(contributorDtos);
        }
        [HttpGet("{contributorId:int}")]
        public async Task<IActionResult> GetContributorById(int contributorId)
        {
            ContributorDto contributorDto = await _contributorService.GetContributorByIdAsync(contributorId);
            return Ok(contributorDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddContributor([FromBody] AddContributorDto addContributorDto)
        {
            Validator.ValidateAndThrow(addContributorDto, new AddContributorValidator());
            ContributorDto contributorDto = await _contributorService.AddContributorAsync(addContributorDto);
            return CreatedAtAction(nameof(GetContributorById), new { contributorId = contributorDto.Id }, ApiResponse<ContributorDto>.Success("Contributor added successfully", contributorDto));
        }
        [HttpPut("{contributorId:int}")]
        public async Task<IActionResult> UpdateContributor(int contributorId, [FromBody] UpdateContributorDto updateContributorDto)
        {
            Validator.ValidateAndThrow(updateContributorDto, new UpdateContributorValidator());
            ContributorDto contributorDto = await _contributorService.UpdateContributorAsync(contributorId, updateContributorDto);
            return Ok(ApiResponse<ContributorDto>.Success(
                "Contributor updated successfully",
                contributorDto
            ));
        }
        [HttpDelete("{contributorId:int}")]
        public async Task<IActionResult> DeleteContributor(int contributorId)
        {
            await _contributorService.DeleteContributorAsync(contributorId);
            return Ok(ApiResponse.Success(
                "Contributor deleted successfully."
            ));
        }
        [HttpPut("{contributorId:int}/profile-image")]
        public async Task<IActionResult> UpdateImageProfile(int contributorId, [FromForm] UpdateProfileImageContributorDto updateProfileImageContributorDto)
        {
            Validator.ValidateAndThrow(updateProfileImageContributorDto, new UpdateContributorProfileImageValidator());
            ContributorDto contributorDto = await _contributorService.UpdateProfileImageContributorAsync(contributorId, updateProfileImageContributorDto);
            return Ok(ApiResponse<ContributorDto>.Success(
                "Contributor profile image updated",
                contributorDto
            ));
        }
    }
}