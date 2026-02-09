using Microsoft.AspNetCore.Mvc;
using Weblu.Application.DTOs.Common.MethodDTOs;
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
            List<MethodDTO> methodDTOs = await _methodService.GetAllAsync(methodParameters);
            return Ok(methodDTOs);
        }
        [HttpGet("{methodId:int}")]
        public async Task<IActionResult> GetById(int methodId)
        {
            MethodDTO methodDTO = await _methodService.GetByIdAsync(methodId);
            return Ok(methodDTO);
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMethodDTO createMethodDTO)
        {
            Validator.ValidateAndThrow(createMethodDTO, new CreateMethodValidator());
            MethodDTO methodDTO = await _methodService.CreateAsync(createMethodDTO);
            return CreatedAtAction(nameof(GetById), new { methodId = methodDTO.Id }, ApiResponse<MethodDTO>.Success
            (
                "Method added successfully.",
                methodDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpPut("{methodId:int}")]
        public async Task<IActionResult> Update(int methodId, [FromBody] UpdateMethodDTO updateMethodDTO)
        {
            Validator.ValidateAndThrow(updateMethodDTO, new UpdateMethodValidator());
            MethodDTO methodDTO = await _methodService.UpdateAsync(methodId, updateMethodDTO);
            return Ok(ApiResponse<MethodDTO>.Success
            (
                "Method updated successfully.",
                 methodDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageMethods)]
        [HttpPut("{methodId:int}/image")]
        public async Task<IActionResult> ChangeImage(int methodId, [FromForm] ChangeMethodImageDTO changeMethodImageDTO)
        {
            Validator.ValidateAndThrow(changeMethodImageDTO, new ChangeMethodImageValidator());
            MethodDTO methodDTO = await _methodService.ChangeImageAsync(methodId, changeMethodImageDTO);
            return Ok(ApiResponse<MethodDTO>.Success
            (
                "Method image updated successfully.",
                methodDTO
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