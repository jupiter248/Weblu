using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Dtos.PortfolioDtos.PortfolioImageDtos;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Parameters;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Portfolios;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IPortfolioFeatureService _portfolioFeatureService;
        private readonly IPortfolioMethodService _portfolioMethodService;
        private readonly IPortfolioContributorService _portfolioContributorService;
        private readonly IPortfolioImageService _portfolioImageService;

        public PortfolioController(
            IPortfolioService portfolioService,
            IPortfolioFeatureService portfolioFeatureService,
            IPortfolioMethodService portfolioMethodService,
            IPortfolioContributorService portfolioContributor,
            IPortfolioImageService portfolioImageService
        )
        {
            _portfolioService = portfolioService;
            _portfolioFeatureService = portfolioFeatureService;
            _portfolioMethodService = portfolioMethodService;
            _portfolioContributorService = portfolioContributor;
            _portfolioImageService = portfolioImageService;
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
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPost]
        public async Task<IActionResult> AddPortfolio([FromBody] AddPortfolioDto addPortfolioDto)
        {
            Validator.ValidateAndThrow(addPortfolioDto, new AddPortfolioValidator());
            PortfolioDetailDto portfolioDetailDto = await _portfolioService.AddPortfolioAsync(addPortfolioDto);
            return CreatedAtAction(nameof(GetPortfolioById), new { portfolioId = portfolioDetailDto.Id }, ApiResponse<PortfolioDetailDto>.Success("Portfolio created successfully", portfolioDetailDto));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
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
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpDelete("{portfolioId:int}")]
        public async Task<IActionResult> DeletePortfolio(int portfolioId)
        {
            await _portfolioService.DeletePortfolioAsync(portfolioId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPost("{portfolioId:int}/feature/{featureId:int}")]
        public async Task<IActionResult> AddFeatureToPortfolio(int portfolioId, int featureId)
        {
            await _portfolioFeatureService.AddFeatureAsync(portfolioId, featureId);
            return Ok(ApiResponse.Success("Feature added successfully"));
        }
        [Authorize(Roles = "Admin")]
        [Authorize(Policy = Permissions.ManagePortfolios)]
        public async Task<IActionResult> DeleteFeatureFromPortfolio(int portfolioId, int featureId)
        {
            await _portfolioFeatureService.DeleteFeatureAsync(portfolioId, featureId);
            return Ok(ApiResponse.Success("Feature deleted successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{portfolioId:int}/method/{methodId:int}")]
        public async Task<IActionResult> AddMethodToPortfolio(int portfolioId, int methodId)
        {
            await _portfolioMethodService.AddMethodAsync(portfolioId, methodId);
            return Ok(ApiResponse.Success("Method added successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpDelete("{portfolioId:int}/method/{methodId:int}")]
        public async Task<IActionResult> DeleteMethodFromPortfolio(int portfolioId, int methodId)
        {
            await _portfolioMethodService.DeleteMethodAsync(portfolioId, methodId);
            return Ok(ApiResponse.Success("Method deleted successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPost("{portfolioId:int}/image/{imageId:int}")]
        public async Task<IActionResult> AddImageToPortfolio(int portfolioId, int imageId, [FromBody] AddPortfolioImageDto addPortfolioImageDto)
        {
            await _portfolioImageService.AddImageAsync(portfolioId, imageId, addPortfolioImageDto);
            return Ok(ApiResponse.Success("Image added successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpDelete("{portfolioId:int}/image/{imageId:int}")]
        public async Task<IActionResult> DeleteImageFromPortfolio(int portfolioId, int imageId)
        {
            await _portfolioImageService.DeleteImageAsync(portfolioId, imageId);
            return Ok(ApiResponse.Success("Image deleted successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPost("{portfolioId:int}/contributor/{contributorId:int}")]
        public async Task<IActionResult> AddContributorToPortfolio(int portfolioId, int contributorId)
        {
            await _portfolioContributorService.AddContributorAsync(portfolioId, contributorId);
            return Ok(ApiResponse.Success("Contributor added successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpDelete("{portfolioId:int}/contributor/{contributorId:int}")]
        public async Task<IActionResult> DeleteContributorFromPortfolio(int portfolioId, int contributorId)
        {
            await _portfolioContributorService.DeleteContributorAsync(portfolioId, contributorId);
            return Ok(ApiResponse.Success("Contributor deleted successfully"));
        }
    }
}