using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.AboutUsDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Application.Validations;
using Weblu.Application.Validations.AboutUsInfo;

namespace Weblu.Api.Controllers
{
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
        public async Task<IActionResult> GetAllAboutUsInfos([FromQuery] AboutUsParameters aboutUsParameters)
        {
            List<AboutUsDto> aboutUsDtos = await _aboutUsService.GetAllAboutUsInfosAsync(aboutUsParameters);
            return Ok(aboutUsDtos);
        }
        [HttpGet("{aboutUsId:int}")]
        public async Task<IActionResult> GetAboutUsById(int aboutUsId)
        {
            AboutUsDto aboutUsDto = await _aboutUsService.GetAboutUsInfoByIdAsync(aboutUsId);
            return Ok(aboutUsDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddAboutUs([FromBody] AddAboutUsDto addAboutUsDto)
        {
            Validator.ValidateAndThrow(addAboutUsDto, new AddAboutUsValidator());
            AboutUsDto aboutUsDto = await _aboutUsService.AddAboutUsAsync(addAboutUsDto);
            return CreatedAtAction(nameof(GetAboutUsById), new { aboutUsId = aboutUsDto.Id }, ApiResponse<AboutUsDto>.Success("AboutUs information added successfully", aboutUsDto));
        }
        [HttpPut("{aboutUsId:int}")]
        public async Task<IActionResult> UpdateAboutUs(int aboutUsId, [FromBody] UpdateAboutUsDto updateAboutUsDto)
        {
            Validator.ValidateAndThrow(updateAboutUsDto, new UpdateAboutUsValidator());
            AboutUsDto aboutUsDto = await _aboutUsService.UpdateAboutUsAsync(aboutUsId, updateAboutUsDto);
            return Ok(ApiResponse<AboutUsDto>.Success(
                "AboutUs information updated successfully",
                aboutUsDto
            ));
        }
        [HttpDelete("{aboutUsId:int}")]
        public async Task<IActionResult> DeleteAboutUs(int aboutUsId)
        {
            await _aboutUsService.DeleteAboutUsAsync(aboutUsId);
            return Ok(ApiResponse.Success(
                "AboutUs information deleted successfully."
            ));
        }
        [HttpPut("{aboutUsId:int}/head-image")]
        public async Task<IActionResult> UpdateImageAboutUs(int aboutUsId, [FromForm] UpdateImageAboutUsDto updateImageAboutUsDto)
        {
            Validator.ValidateAndThrow(updateImageAboutUsDto, new UpdateImageAboutUsValidator());
            AboutUsDto aboutUsDto = await _aboutUsService.UpdateHeadImageAboutUsAsync(aboutUsId, updateImageAboutUsDto);
            return Ok(ApiResponse<AboutUsDto>.Success(
                "AboutUs head image updated",
                aboutUsDto
            ));
        }
        [HttpDelete("{aboutUsId:int}/head-image")]
        public async Task<IActionResult> DeleteAboutUsHeadImage(int aboutUsId)
        {
            await _aboutUsService.DeleteAboutUsHeadImageAsync(aboutUsId);
            return Ok(ApiResponse.Success(
                "AboutUs head image deleted successfully."
            ));
        }
    }
}