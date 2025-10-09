using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Dtos.MethodDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Methods;
using Weblu.Domain.Parameters;

namespace Weblu.Api.Controllers
{
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
        [HttpPost]
        public async Task<IActionResult> AddMethod([FromBody] AddMethodDto addMethodDto)
        {
            Validator.ValidateAndThrow(addMethodDto, new AddMethodValidator());
            MethodDto methodDto = await _methodService.AddMethodAsync(addMethodDto);
            return CreatedAtAction(nameof(GetMethodById), new { methodId = methodDto.Id }, new
            {
                message = "Method added successfully.",
                method = methodDto
            });
        }
        [HttpPut("{methodId:int}")]
        public async Task<IActionResult> UpdateMethod(int methodId, [FromBody] UpdateMethodDto updateMethodDto)
        {
            Validator.ValidateAndThrow(updateMethodDto, new UpdateMethodValidator());
            MethodDto methodDto = await _methodService.UpdateMethodAsync(methodId, updateMethodDto);
            return Ok(new
            {
                message = "Method added successfully.",
                method = methodDto
            });
        }
        [HttpDelete("{methodId:int}")]
        public async Task<IActionResult> DeleteMethod(int methodId)
        {
            await _methodService.DeleteMethodAsync(methodId);
            return NoContent();
        }
    }
}