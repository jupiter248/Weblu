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
using Weblu.Domain.Parameters;

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
        [HttpPost]
        public async Task<IActionResult> AddFeature([FromBody] AddFeatureDto addFeatureDto)
        {
            Validator.ValidateAndThrow(addFeatureDto, new AddFeatureValidator());
            FeatureDto featureDto = await _featureService.AddFeatureAsync(addFeatureDto);
            return CreatedAtAction(nameof(GetFeatureById), new { featureId = featureDto.Id }, new
            {
                message = "Feature added successfully.",
                feature = featureDto
            });
        }
        [HttpPut("{featureId:int}")]
        public async Task<IActionResult> UpdateFeature(int featureId, [FromBody] UpdateFeatureDto updateFeatureDto)
        {
            Validator.ValidateAndThrow(updateFeatureDto, new UpdateFeatureValidator());
            FeatureDto featureDto = await _featureService.UpdateFeatureAsync(featureId, updateFeatureDto);
            return Ok(new
            {
                message = "Feature added successfully.",
                feature = featureDto
            });
        }
        [HttpDelete("{featureId:int}")]
        public async Task<IActionResult> DeleteFeature(int featureId)
        {
            await _featureService.DeleteFeatureAsync(featureId);
            return NoContent();
        }
    }
}