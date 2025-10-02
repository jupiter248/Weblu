using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Services;
using Weblu.Domain.Parameters;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/service")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllServices([FromQuery] ServiceParameters serviceParameters)
        {
            List<ServiceDto> serviceDtos = await _serviceService.GetAllServicesAsync(serviceParameters);
            return Ok(serviceDtos);
        }
        [HttpGet("{serviceId:int}")]
        public async Task<IActionResult> GetServiceById(int serviceId)
        {
            ServiceDto serviceDto = await _serviceService.GetServiceByIdAsync(serviceId);
            return Ok(serviceDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddService([FromBody] AddServiceDto addServiceDto)
        {
            Validator.ValidateAndThrow(addServiceDto, new AddServiceValidator());
            ServiceDto serviceDto = await _serviceService.AddServiceAsync(addServiceDto);
            return CreatedAtAction(nameof(GetServiceById), new { serviceId = serviceDto.Id }, new { message = "Service added successfully", service = serviceDto });
        }
        [HttpPut("{serviceId:int}")]
        public async Task<IActionResult> UpdateService(int serviceId, [FromBody] UpdateServiceDto updateServiceDto)
        {
            Validator.ValidateAndThrow(updateServiceDto, new UpdateServiceValidator());
            ServiceDto serviceDto = await _serviceService.UpdateServiceAsync(serviceId, updateServiceDto);
            return Ok(
                new
                {
                    message = "Service updated successfully",
                    service = serviceDto
                }
            );
        }
        [HttpDelete("{serviceId:int}")]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            await _serviceService.DeleteServiceAsync(serviceId);
            return NoContent();
        }
    }
}