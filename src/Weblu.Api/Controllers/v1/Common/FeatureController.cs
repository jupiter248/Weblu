using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Common.FeatureDtos;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Common.Features;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Common
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/feature")]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFeatures([FromQuery] FeatureParameters featureParameters)
        {
            List<FeatureDto> featureDtos = await _featureService.GetAllFeaturesAsync(featureParameters);
            return Ok(featureDtos);
        }
        [HttpGet("{featureId:int}")]
        public async Task<IActionResult> GetFeatureById(int featureId)
        {
            FeatureDto featureDto = await _featureService.GetFeatureByIdAsync(featureId);
            return Ok(featureDto);
        }
        [Authorize(Policy = Permissions.ManageFeatures)]
        [HttpPost]
        public async Task<IActionResult> AddFeature([FromBody] AddFeatureDto addFeatureDto)
        {
            Validator.ValidateAndThrow(addFeatureDto, new AddFeatureValidator());
            FeatureDto featureDto = await _featureService.AddFeatureAsync(addFeatureDto);
            return CreatedAtAction(nameof(GetFeatureById), new { featureId = featureDto.Id }, ApiResponse<FeatureDto>.Success
            (
                "Feature created successfully.",
                featureDto
            ));
        }
        [Authorize(Policy = Permissions.ManageFeatures)]
        [HttpPut("{featureId:int}")]
        public async Task<IActionResult> UpdateFeature(int featureId, [FromBody] UpdateFeatureDto updateFeatureDto)
        {
            Validator.ValidateAndThrow(updateFeatureDto, new UpdateFeatureValidator());
            FeatureDto featureDto = await _featureService.UpdateFeatureAsync(featureId, updateFeatureDto);
            return Ok(ApiResponse<FeatureDto>.Success
            (
                "Feature updated successfully.",
                featureDto
            ));
        }
        [Authorize(Policy = Permissions.ManageFeatures)]
        [HttpDelete("{featureId:int}")]
        public async Task<IActionResult> DeleteFeature(int featureId)
        {
            await _featureService.DeleteFeatureAsync(featureId);
            return Ok(ApiResponse.Success
            (
                "Feature deleted successfully."
            ));
        }
    }
}