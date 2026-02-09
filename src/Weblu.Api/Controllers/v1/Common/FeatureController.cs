using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Common.FeatureDTOs;
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
        public async Task<IActionResult> GetAll([FromQuery] FeatureParameters featureParameters)
        {
            List<FeatureDTO> featureDTOs = await _featureService.GetAllAsync(featureParameters);
            return Ok(featureDTOs);
        }
        [HttpGet("{featureId:int}")]
        public async Task<IActionResult> GetById(int featureId)
        {
            FeatureDTO featureDTO = await _featureService.GetByIdAsync(featureId);
            return Ok(featureDTO);
        }
        [Authorize(Policy = Permissions.ManageFeatures)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeatureDTO createFeatureDTO)
        {
            Validator.ValidateAndThrow(createFeatureDTO, new CreateFeatureValidator());
            FeatureDTO featureDTO = await _featureService.CreateAsync(createFeatureDTO);
            return CreatedAtAction(nameof(GetById), new { featureId = featureDTO.Id }, ApiResponse<FeatureDTO>.Success
            (
                "Feature created successfully.",
                featureDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageFeatures)]
        [HttpPut("{featureId:int}")]
        public async Task<IActionResult> Update(int featureId, [FromBody] UpdateFeatureDTO updateFeatureDTO)
        {
            Validator.ValidateAndThrow(updateFeatureDTO, new UpdateFeatureValidator());
            FeatureDTO featureDTO = await _featureService.UpdateAsync(featureId, updateFeatureDTO);
            return Ok(ApiResponse<FeatureDTO>.Success
            (
                "Feature updated successfully.",
                featureDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageFeatures)]
        [HttpDelete("{featureId:int}")]
        public async Task<IActionResult> Delete(int featureId)
        {
            await _featureService.DeleteAsync(featureId);
            return Ok(ApiResponse.Success
            (
                "Feature deleted successfully."
            ));
        }
    }
}