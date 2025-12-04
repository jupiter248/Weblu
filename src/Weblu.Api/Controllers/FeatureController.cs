using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Dtos.FeatureDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Features;
using Weblu.Domain.Entities;
using Weblu.Application.Parameters;
using Weblu.Application.Common.Responses;
using Microsoft.AspNetCore.Authorization;

namespace Weblu.Api.Controllers
{
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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