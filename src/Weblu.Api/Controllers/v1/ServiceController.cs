using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Application.Dtos.ServiceDtos.ServiceImageDtos;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Services;
using Weblu.Application.Parameters;
using Weblu.Application.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Asp.Versioning;

namespace Weblu.Api.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/service")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly IServiceFeatureService _serviceFeatureService;
        private readonly IServiceMethodService _serviceMethodService;
        private readonly IServiceImageService _serviceImageService;

        public ServiceController(
            IServiceService serviceService,
            IServiceFeatureService serviceFeatureService,
            IServiceMethodService serviceMethodService,
            IServiceImageService serviceImageService
        )
        {
            _serviceService = serviceService;
            _serviceFeatureService = serviceFeatureService;
            _serviceMethodService = serviceMethodService;
            _serviceImageService = serviceImageService;
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddService([FromBody] AddServiceDto addServiceDto)
        {
            Validator.ValidateAndThrow(addServiceDto, new AddServiceValidator());
            ServiceDetailDto serviceDto = await _serviceService.AddServiceAsync(addServiceDto);
            return CreatedAtAction(nameof(GetServiceById), new { serviceId = serviceDto.Id }, ApiResponse<ServiceDetailDto>.Success("Service created successfully", serviceDto));
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("{serviceId:int}")]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            await _serviceService.DeleteServiceAsync(serviceId);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{serviceId:int}/feature/{featureId:int}")]
        public async Task<IActionResult> AddFeatureToService(int serviceId, int featureId)
        {
            await _serviceFeatureService.AddFeatureAsync(serviceId, featureId);
            return Ok(ApiResponse.Success("Feature added successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{serviceId:int}/feature/{featureId:int}")]
        public async Task<IActionResult> DeleteFeatureFromService(int serviceId, int featureId)
        {
            await _serviceFeatureService.DeleteFeatureAsync(serviceId, featureId);
            return Ok(ApiResponse.Success("Feature deleted successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{serviceId:int}/method/{methodId:int}")]
        public async Task<IActionResult> AddMethodToService(int serviceId, int methodId)
        {
            await _serviceMethodService.AddMethodAsync(serviceId, methodId);
            return Ok(ApiResponse.Success("Method added successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{serviceId:int}/method/{methodId:int}")]
        public async Task<IActionResult> DeleteMethodFromService(int serviceId, int methodId)
        {
            await _serviceMethodService.DeleteMethodAsync(serviceId, methodId);
            return Ok(ApiResponse.Success("Method deleted successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{serviceId:int}/image/{imageId:int}")]
        public async Task<IActionResult> AddImageToService(int serviceId, int imageId, [FromBody] AddServiceImageDto addServiceImageDto)
        {
            await _serviceImageService.AddImageAsync(serviceId, imageId, addServiceImageDto);
            return Ok(ApiResponse.Success("Image added successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{serviceId:int}/image/{imageId:int}")]
        public async Task<IActionResult> DeleteImageFromService(int serviceId, int imageId)
        {
            await _serviceImageService.DeleteImageAsync(serviceId, imageId);
            return Ok(ApiResponse.Success("Image deleted successfully"));
        }
    }
}