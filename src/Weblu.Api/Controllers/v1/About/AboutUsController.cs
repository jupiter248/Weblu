using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.About.AboutUsDtos;
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
        public async Task<IActionResult> GetAll([FromQuery] AboutUsParameters aboutUsParameters)
        {
            List<AboutUsDto> aboutUsDtos = await _aboutUsService.GetAllAsync(aboutUsParameters);
            return Ok(aboutUsDtos);
        }
        [HttpGet("{aboutUsId:int}")]
        public async Task<IActionResult> GetById(int aboutUsId)
        {
            AboutUsDto aboutUsDto = await _aboutUsService.GetByIdAsync(aboutUsId);
            return Ok(aboutUsDto);
        }
        [Authorize(Policy = Permissions.ManageAboutUs)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAboutUsDto createAboutUsDto)
        {
            Validator.ValidateAndThrow(createAboutUsDto, new CreateAboutUsValidator());
            AboutUsDto aboutUsDto = await _aboutUsService.CreateAsync(createAboutUsDto);
            return CreatedAtAction(nameof(GetById), new { aboutUsId = aboutUsDto.Id }, ApiResponse<AboutUsDto>.Success("AboutUs information added successfully", aboutUsDto));
        }
        [Authorize(Policy = Permissions.ManageAboutUs)]
        [HttpPut("{aboutUsId:int}")]
        public async Task<IActionResult> Update(int aboutUsId, [FromBody] UpdateAboutUsDto updateAboutUsDto)
        {
            Validator.ValidateAndThrow(updateAboutUsDto, new UpdateAboutUsValidator());
            AboutUsDto aboutUsDto = await _aboutUsService.UpdateAsync(aboutUsId, updateAboutUsDto);
            return Ok(ApiResponse<AboutUsDto>.Success(
                "AboutUs information updated successfully",
                aboutUsDto
            ));
        }
        [Authorize(Policy = Permissions.ManageAboutUs)]
        [HttpDelete("{aboutUsId:int}")]
        public async Task<IActionResult> Delete(int aboutUsId)
        {
            await _aboutUsService.DeleteAsync(aboutUsId);
            return Ok(ApiResponse.Success(
                "AboutUs information deleted successfully."
            ));
        }
        [Authorize(Policy = Permissions.ManageAboutUs)]
        [HttpPut("{aboutUsId:int}/head-image")]
        public async Task<IActionResult> UpdateImage(int aboutUsId, [FromForm] ChangeAboutUsImageDto changeAboutUsImageDto)
        {
            Validator.ValidateAndThrow(changeAboutUsImageDto, new ChangeAboutUsImageValidator());
            AboutUsDto aboutUsDto = await _aboutUsService.ChangeHeadImageAsync(aboutUsId, changeAboutUsImageDto);
            return Ok(ApiResponse<AboutUsDto>.Success(
                "AboutUs head image updated",
                aboutUsDto
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