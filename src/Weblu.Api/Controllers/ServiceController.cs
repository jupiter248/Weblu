using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Application.Dtos.ServiceDtos.ServiceImageDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Services;
using Weblu.Application.Parameters;
using Weblu.Application.Common.Responses;

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
            List<ServiceSummaryDto> serviceDtos = await _serviceService.GetAllServicesAsync(serviceParameters);
            return Ok(serviceDtos);
        }
        [HttpGet("{serviceId:int}")]
        public async Task<IActionResult> GetServiceById(int serviceId)
        {
            ServiceDetailDto serviceDto = await _serviceService.GetServiceByIdAsync(serviceId);
            return Ok(serviceDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddService([FromBody] AddServiceDto addServiceDto)
        {
            Validator.ValidateAndThrow(addServiceDto, new AddServiceValidator());
            ServiceDetailDto serviceDto = await _serviceService.AddServiceAsync(addServiceDto);
            return CreatedAtAction(nameof(GetServiceById), new { serviceId = serviceDto.Id }, ApiResponse<ServiceDetailDto>.Success("Service created successfully", serviceDto));
        }
        [HttpPut("{serviceId:int}")]
        public async Task<IActionResult> UpdateService(int serviceId, [FromBody] UpdateServiceDto updateServiceDto)
        {
            Validator.ValidateAndThrow(updateServiceDto, new UpdateServiceValidator());
            ServiceDetailDto serviceDto = await _serviceService.UpdateServiceAsync(serviceId, updateServiceDto);
            return Ok(
                ApiResponse<ServiceDetailDto>.Success
                (
                    "Service updated successfully",
                    serviceDto
                )
            );
        }
        [HttpDelete("{serviceId:int}")]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            await _serviceService.DeleteServiceAsync(serviceId);
            return NoContent();
        }
        [HttpPost("{serviceId:int}/feature/{featureId:int}")]
        public async Task<IActionResult> AddFeatureToService(int serviceId, int featureId)
        {
            await _serviceService.AddFeatureToServiceAsync(serviceId, featureId);
            return Ok(ApiResponse.Success("Feature added successfully"));
        }
        [HttpDelete("{serviceId:int}/feature/{featureId:int}")]
        public async Task<IActionResult> DeleteFeatureFromService(int serviceId, int featureId)
        {
            await _serviceService.DeleteFeatureFromServiceAsync(serviceId, featureId);
            return Ok(ApiResponse.Success("Feature deleted successfully"));
        }
        [HttpPost("{serviceId:int}/method/{methodId:int}")]
        public async Task<IActionResult> AddMethodToService(int serviceId, int methodId)
        {
            await _serviceService.AddMethodToService(serviceId, methodId);
            return Ok(ApiResponse.Success("Method added successfully"));
        }
        [HttpDelete("{serviceId:int}/method/{methodId:int}")]
        public async Task<IActionResult> DeleteMethodFromService(int serviceId, int methodId)
        {
            await _serviceService.DeleteMethodFromService(serviceId, methodId);
            return Ok(ApiResponse.Success("Method deleted successfully"));
        }
        [HttpPost("{serviceId:int}/image/{imageId:int}")]
        public async Task<IActionResult> AddImageToService(int serviceId, int imageId, [FromBody] AddServiceImageDto addServiceImageDto)
        {
            await _serviceService.AddImageToService(serviceId, imageId, addServiceImageDto);
            return Ok(ApiResponse.Success("Image added successfully"));
        }
        [HttpDelete("{serviceId:int}/image/{imageId:int}")]
        public async Task<IActionResult> DeleteImageFromService(int serviceId, int imageId)
        {
            await _serviceService.DeleteImageToService(serviceId, imageId);
            return Ok(ApiResponse.Success("Image deleted successfully"));
        }
    }
}