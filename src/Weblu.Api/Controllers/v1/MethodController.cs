using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Dtos.MethodDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Methods;
using Weblu.Application.Parameters;
using Weblu.Application.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1
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
        public async Task<IActionResult> GetAllMethods([FromQuery] MethodParameters methodParameters)
        {
            List<MethodDto> methodDtos = await _methodService.GetAllMethodsAsync(methodParameters);
            return Ok(methodDtos);
        }
        [HttpGet("{methodId:int}")]
        public async Task<IActionResult> GetMethodById(int methodId)
        {
            MethodDto methodDto = await _methodService.GetMethodByIdAsync(methodId);
            return Ok(methodDto);
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpPost]
        public async Task<IActionResult> AddMethod([FromBody] AddMethodDto addMethodDto)
        {
            Validator.ValidateAndThrow(addMethodDto, new AddMethodValidator());
            MethodDto methodDto = await _methodService.AddMethodAsync(addMethodDto);
            return CreatedAtAction(nameof(GetMethodById), new { methodId = methodDto.Id }, ApiResponse<MethodDto>.Success
            (
                "Method added successfully.",
                methodDto
            ));
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpPut("{methodId:int}")]
        public async Task<IActionResult> UpdateMethod(int methodId, [FromBody] UpdateMethodDto updateMethodDto)
        {
            Validator.ValidateAndThrow(updateMethodDto, new UpdateMethodValidator());
            MethodDto methodDto = await _methodService.UpdateMethodAsync(methodId, updateMethodDto);
            return Ok(ApiResponse<MethodDto>.Success
            (
                "Method updated successfully.",
                 methodDto
            ));
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpPut("{methodId:int}/image")]
        public async Task<IActionResult> UpdateMethodImage(int methodId, [FromForm] UpdateMethodImageDto updateMethodImageDto)
        {
            Validator.ValidateAndThrow(updateMethodImageDto, new UpdateMethodImageValidator());
            MethodDto methodDto = await _methodService.UpdateMethodImageAsync(methodId, updateMethodImageDto);
            return Ok(ApiResponse<MethodDto>.Success
            (
                "Method image updated successfully.",
                methodDto
            ));
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpDelete("{methodId:int}")]
        public async Task<IActionResult> DeleteMethod(int methodId)
        {
            await _methodService.DeleteMethodAsync(methodId);
            return Ok(ApiResponse.Success
            (
                "Method deleted successfully."
            ));
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpDelete("{methodId:int}/image")]
        public async Task<IActionResult> DeleteMethodImage(int methodId)
        {
            await _methodService.DeleteMethodImageAsync(methodId);
            return Ok(ApiResponse.Success
            (
                "Method Image deleted successfully."
            ));
        }
    }
}