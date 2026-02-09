using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.About.AboutUsDTOs;
using Weblu.Application.Interfaces.Services.About;
using Weblu.Application.Parameters.About;
using Weblu.Application.Validations;
using Weblu.Application.Validations.About.AboutUs;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.About
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/about-us")]
    public class AboutUsController : ControllerBase
    {
        private readonly IAboutUsService _aboutUsService;
        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            AboutUsDTO aboutUsDTO = await _aboutUsService.GetAsync();
            return Ok(ApiResponse<AboutUsDTO>.Success(
                "AboutUs information retrieved successfully",
                aboutUsDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageAboutUs)]
        [HttpPut("{aboutUsId:int}")]
        public async Task<IActionResult> Update(int aboutUsId, [FromBody] UpdateAboutUsDTO updateAboutUsDTO)
        {
            Validator.ValidateAndThrow(updateAboutUsDTO, new UpdateAboutUsValidator());
            AboutUsDTO aboutUsDTO = await _aboutUsService.UpdateAsync(aboutUsId, updateAboutUsDTO);
            return Ok(ApiResponse<AboutUsDTO>.Success(
                "AboutUs information updated successfully",
                aboutUsDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageAboutUs)]
        [HttpPut("{aboutUsId:int}/head-image")]
        public async Task<IActionResult> UpdateImage(int aboutUsId, [FromForm] ChangeAboutUsImageDTO changeAboutUsImageDTO)
        {
            Validator.ValidateAndThrow(changeAboutUsImageDTO, new ChangeAboutUsImageValidator());
            AboutUsDTO aboutUsDTO = await _aboutUsService.ChangeHeadImageAsync(aboutUsId, changeAboutUsImageDTO);
            return Ok(ApiResponse<AboutUsDTO>.Success(
                "AboutUs head image updated",
                aboutUsDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageAboutUs)]
        [HttpDelete("{aboutUsId:int}/head-image")]
        public async Task<IActionResult> DeleteImage(int aboutUsId)
        {
            await _aboutUsService.DeleteHeadImageAsync(aboutUsId);
            return Ok(ApiResponse.Success(
                "AboutUs head image deleted successfully."
            ));
        }
    }
}