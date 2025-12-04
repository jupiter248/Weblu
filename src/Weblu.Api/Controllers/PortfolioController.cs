using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Dtos.PortfolioDtos.PortfolioImageDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Portfolios;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPortfolios([FromQuery] PortfolioParameters portfolioParameters)
        {
            List<PortfolioSummaryDto> portfolioSummaryDtos = await _portfolioService.GetAllPortfolioAsync(portfolioParameters);
            return Ok(portfolioSummaryDtos);
        }
        [HttpGet("{portfolioId:int}")]
        public async Task<IActionResult> GetPortfolioById(int portfolioId)
        {
            PortfolioDetailDto portfolioDetailDto = await _portfolioService.GetPortfolioByIdAsync(portfolioId);
            return Ok(portfolioDetailDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddPortfolio([FromBody] AddPortfolioDto addPortfolioDto)
        {
            Validator.ValidateAndThrow(addPortfolioDto, new AddPortfolioValidator());
            PortfolioDetailDto portfolioDetailDto = await _portfolioService.AddPortfolioAsync(addPortfolioDto);
            return CreatedAtAction(nameof(GetPortfolioById), new { portfolioId = portfolioDetailDto.Id }, ApiResponse<PortfolioDetailDto>.Success("Portfolio created successfully", portfolioDetailDto));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{portfolioId:int}")]
        public async Task<IActionResult> UpdatePortfolio(int portfolioId, [FromBody] UpdatePortfolioDto updatePortfolioDto)
        {
            Validator.ValidateAndThrow(updatePortfolioDto, new UpdatePortfolioValidator());
            PortfolioDetailDto portfolioDetailDto = await _portfolioService.UpdatePortfolioAsync(portfolioId, updatePortfolioDto);
            return Ok(
                ApiResponse<PortfolioDetailDto>.Success
                (
                    "Portfolio updated successfully",
                    portfolioDetailDto
                )
            );
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{portfolioId:int}")]
        public async Task<IActionResult> DeletePortfolio(int portfolioId)
        {
            await _portfolioService.DeletePortfolioAsync(portfolioId);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{portfolioId:int}/feature/{featureId:int}")]
        public async Task<IActionResult> AddFeatureToPortfolio(int portfolioId, int featureId)
        {
            await _portfolioService.AddFeatureToPortfolioAsync(portfolioId, featureId);
            return Ok(ApiResponse.Success("Feature added successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{portfolioId:int}/feature/{featureId:int}")]
        public async Task<IActionResult> DeleteFeatureFromPortfolio(int portfolioId, int featureId)
        {
            await _portfolioService.DeleteFeatureFromPortfolioAsync(portfolioId, featureId);
            return Ok(ApiResponse.Success("Feature deleted successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{portfolioId:int}/method/{methodId:int}")]
        public async Task<IActionResult> AddMethodToPortfolio(int portfolioId, int methodId)
        {
            await _portfolioService.AddMethodToPortfolioAsync(portfolioId, methodId);
            return Ok(ApiResponse.Success("Method added successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{portfolioId:int}/method/{methodId:int}")]
        public async Task<IActionResult> DeleteMethodFromPortfolio(int portfolioId, int methodId)
        {
            await _portfolioService.DeleteMethodFromPortfolioAsync(portfolioId, methodId);
            return Ok(ApiResponse.Success("Method deleted successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{portfolioId:int}/image/{imageId:int}")]
        public async Task<IActionResult> AddImageToPortfolio(int portfolioId, int imageId, [FromBody] AddPortfolioImageDto addPortfolioImageDto)
        {
            await _portfolioService.AddImageToPortfolioAsync(portfolioId, imageId, addPortfolioImageDto);
            return Ok(ApiResponse.Success("Image added successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{portfolioId:int}/image/{imageId:int}")]
        public async Task<IActionResult> DeleteImageFromPortfolio(int portfolioId, int imageId)
        {
            await _portfolioService.DeleteImageFromPortfolioAsync(portfolioId, imageId);
            return Ok(ApiResponse.Success("Image deleted successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{portfolioId:int}/contributor/{contributorId:int}")]
        public async Task<IActionResult> AddContributorToPortfolio(int portfolioId, int contributorId)
        {
            await _portfolioService.AddContributorToPortfolioAsync(portfolioId, contributorId);
            return Ok(ApiResponse.Success("Contributor added successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{portfolioId:int}/contributor/{contributorId:int}")]
        public async Task<IActionResult> DeleteContributorFromPortfolio(int portfolioId, int contributorId)
        {
            await _portfolioService.DeleteContributorFromPortfolioAsync(portfolioId, contributorId);
            return Ok(ApiResponse.Success("Contributor deleted successfully"));
        }
    }
}