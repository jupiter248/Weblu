using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Dtos.Services.ServiceDtos;
using Weblu.Application.Dtos.Services.ServiceDtos.ServiceImageDtos;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Services;
using Weblu.Application.Parameters.Services;
using Weblu.Application.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Asp.Versioning;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Services
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
        public async Task<IActionResult> GetAll([FromQuery] ServiceParameters serviceParameters)
        {
            List<ServiceSummaryDto> serviceDtos = await _serviceService.GetAllAsync(serviceParameters);
            return Ok(serviceDtos);
        }
        [HttpGet("{serviceId:int}")]
        public async Task<IActionResult> GetById(int serviceId)
        {
            ServiceDetailDto serviceDto = await _serviceService.GetByIdAsync(serviceId);
            return Ok(serviceDto);
        }
        [Authorize(Policy = Permissions.ManageServices)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceDto createServiceDto)
        {
            Validator.ValidateAndThrow(createServiceDto, new CreateServiceValidator());
            ServiceDetailDto serviceDto = await _serviceService.CreateAsync(createServiceDto);
            return CreatedAtAction(nameof(GetById), new { serviceId = serviceDto.Id }, ApiResponse<ServiceDetailDto>.Success("Service created successfully", serviceDto));
        }
        [Authorize(Policy = Permissions.ManageServices)]
        [HttpPut("{serviceId:int}")]
        public async Task<IActionResult> Update(int serviceId, [FromBody] UpdateServiceDto updateServiceDto)
        {
            Validator.ValidateAndThrow(updateServiceDto, new UpdateServiceValidator());
            ServiceDetailDto serviceDto = await _serviceService.UpdateAsync(serviceId, updateServiceDto);
            return Ok(
                ApiResponse<ServiceDetailDto>.Success
                (
                    "Service updated successfully",
                    serviceDto
                )
            );
        }
        [Authorize(Policy = Permissions.ManageServices)]
        [HttpDelete("{serviceId:int}")]
        public async Task<IActionResult> Delete(int serviceId)
        {
            await _serviceService.DeleteAsync(serviceId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManageServices)]
        [HttpPut("{articleId:int}/publish")]
        public async Task<IActionResult> Publish(int articleId)
        {
            await _serviceService.Publish(articleId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManageServices)]
        [HttpPut("{articleId:int}/unpublish")]
        public async Task<IActionResult> Unpublish(int articleId)
        {
            await _serviceService.Unpublish(articleId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManageServices)]
        [HttpPost("{serviceId:int}/feature/{featureId:int}")]
        public async Task<IActionResult> AddFeature(int serviceId, int featureId)
        {
            await _serviceFeatureService.AddAsync(serviceId, featureId);
            return Ok(ApiResponse.Success("Feature added successfully"));
        }
        [Authorize(Policy = Permissions.ManageServices)]
        [HttpDelete("{serviceId:int}/feature/{featureId:int}")]
        public async Task<IActionResult> DeleteFeature(int serviceId, int featureId)
        {
            await _serviceFeatureService.DeleteAsync(serviceId, featureId);
            return Ok(ApiResponse.Success("Feature deleted successfully"));
        }
        [Authorize(Policy = Permissions.ManageServices)]
        [HttpPost("{serviceId:int}/method/{methodId:int}")]
        public async Task<IActionResult> AddMethod(int serviceId, int methodId)
        {
            await _serviceMethodService.AddAsync(serviceId, methodId);
            return Ok(ApiResponse.Success("Method added successfully"));
        }
        [Authorize(Policy = Permissions.ManageServices)]
        [HttpDelete("{serviceId:int}/method/{methodId:int}")]
        public async Task<IActionResult> DeleteMethod(int serviceId, int methodId)
        {
            await _serviceMethodService.DeleteAsync(serviceId, methodId);
            return Ok(ApiResponse.Success("Method deleted successfully"));
        }
        [Authorize(Policy = Permissions.ManageServices)]
        [HttpPost("{serviceId:int}/image/{imageId:int}")]
        public async Task<IActionResult> AddImage(int serviceId, int imageId, [FromBody] AddServiceImageDto addServiceImageDto)
        {
            await _serviceImageService.AddAsync(serviceId, imageId, addServiceImageDto);
            return Ok(ApiResponse.Success("Image added successfully"));
        }
        [Authorize(Policy = Permissions.ManageServices)]
        [HttpDelete("{serviceId:int}/image/{imageId:int}")]
        public async Task<IActionResult> DeleteImage(int serviceId, int imageId)
        {
            await _serviceImageService.DeleteAsync(serviceId, imageId);
            return Ok(ApiResponse.Success("Image deleted successfully"));
        }
    }
}