using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Dtos.Common.MethodDtos;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Common.Methods;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Common
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/method")]
    public class MethodController : ControllerBase
    {
        private readonly IMethodService _methodService;
        public MethodController(IMethodService methodService)
        {
            _methodService = methodService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MethodParameters methodParameters)
        {
            List<MethodDto> methodDtos = await _methodService.GetAllAsync(methodParameters);
            return Ok(methodDtos);
        }
        [HttpGet("{methodId:int}")]
        public async Task<IActionResult> GetById(int methodId)
        {
            MethodDto methodDto = await _methodService.GetByIdAsync(methodId);
            return Ok(methodDto);
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMethodDto createMethodDto)
        {
            Validator.ValidateAndThrow(createMethodDto, new CreateMethodValidator());
            MethodDto methodDto = await _methodService.CreateAsync(createMethodDto);
            return CreatedAtAction(nameof(GetById), new { methodId = methodDto.Id }, ApiResponse<MethodDto>.Success
            (
                "Method added successfully.",
                methodDto
            ));
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpPut("{methodId:int}")]
        public async Task<IActionResult> Update(int methodId, [FromBody] UpdateMethodDto updateMethodDto)
        {
            Validator.ValidateAndThrow(updateMethodDto, new UpdateMethodValidator());
            MethodDto methodDto = await _methodService.UpdateAsync(methodId, updateMethodDto);
            return Ok(ApiResponse<MethodDto>.Success
            (
                "Method updated successfully.",
                 methodDto
            ));
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpPut("{methodId:int}/image")]
        public async Task<IActionResult> ChangeImage(int methodId, [FromForm] ChangeMethodImageDto changeMethodImageDto)
        {
            Validator.ValidateAndThrow(changeMethodImageDto, new ChangeMethodImageValidator());
            MethodDto methodDto = await _methodService.ChangeImageAsync(methodId, changeMethodImageDto);
            return Ok(ApiResponse<MethodDto>.Success
            (
                "Method image updated successfully.",
                methodDto
            ));
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpDelete("{methodId:int}")]
        public async Task<IActionResult> Delete(int methodId)
        {
            await _methodService.DeleteAsync(methodId);
            return Ok(ApiResponse.Success
            (
                "Method deleted successfully."
            ));
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpDelete("{methodId:int}/image")]
        public async Task<IActionResult> DeleteImage(int methodId)
        {
            await _methodService.DeleteImageAsync(methodId);
            return Ok(ApiResponse.Success
            (
                "Method Image deleted successfully."
            ));
        }
    }
}