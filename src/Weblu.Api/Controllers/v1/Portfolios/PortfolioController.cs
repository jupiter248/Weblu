using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Portfolios.PortfolioDTOs;
using Weblu.Application.DTOs.Portfolios.PortfolioDTOs.PortfolioImageDTOs;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Parameters.Portfolios;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Portfolios.Portfolio;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Portfolios
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
        public async Task<IActionResult> GetAll([FromQuery] PortfolioParameters portfolioParameters)
        {
            List<PortfolioSummaryDTO> portfolioSummaryDTOs = await _portfolioService.GetAllAsync(portfolioParameters);
            return Ok(portfolioSummaryDTOs);
        }
        [HttpGet("{portfolioId:int}")]
        public async Task<IActionResult> GetById(int portfolioId)
        {
            PortfolioDetailDTO portfolioDetailDTO = await _portfolioService.GetByIdAsync(portfolioId);
            return Ok(portfolioDetailDTO);
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePortfolioDTO createPortfolioDTO)
        {
            Validator.ValidateAndThrow(createPortfolioDTO, new CreatePortfolioValidator());
            PortfolioDetailDTO portfolioDetailDTO = await _portfolioService.CreateAsync(createPortfolioDTO);
            return CreatedAtAction(nameof(GetById), new { portfolioId = portfolioDetailDTO.Id }, ApiResponse<PortfolioDetailDTO>.Success("Portfolio created successfully", portfolioDetailDTO));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPut("{portfolioId:int}")]
        public async Task<IActionResult> Update(int portfolioId, [FromBody] UpdatePortfolioDTO updatePortfolioDTO)
        {
            Validator.ValidateAndThrow(updatePortfolioDTO, new UpdatePortfolioValidator());
            PortfolioDetailDTO portfolioDetailDTO = await _portfolioService.UpdateAsync(portfolioId, updatePortfolioDTO);
            return Ok(
                ApiResponse<PortfolioDetailDTO>.Success
                (
                    "Portfolio updated successfully",
                    portfolioDetailDTO
                )
            );
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpDelete("{portfolioId:int}")]
        public async Task<IActionResult> Delete(int portfolioId)
        {
            await _portfolioService.DeleteAsync(portfolioId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPut("{articleId:int}/publish")]
        public async Task<IActionResult> Publish(int articleId)
        {
            await _portfolioService.Publish(articleId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPut("{articleId:int}/unpublish")]
        public async Task<IActionResult> Unpublish(int articleId)
        {
            await _portfolioService.Unpublish(articleId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPost("{portfolioId:int}/feature/{featureId:int}")]
        public async Task<IActionResult> AddFeature(int portfolioId, int featureId)
        {
            await _portfolioFeatureService.AddAsync(portfolioId, featureId);
            return Ok(ApiResponse.Success("Feature added successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpDelete("{portfolioId:int}/feature/{featureId:int}")]
        public async Task<IActionResult> DeleteFeature(int portfolioId, int featureId)
        {
            await _portfolioFeatureService.DeleteAsync(portfolioId, featureId);
            return Ok(ApiResponse.Success("Feature deleted successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPost("{portfolioId:int}/method/{methodId:int}")]
        public async Task<IActionResult> AddMethod(int portfolioId, int methodId)
        {
            await _portfolioMethodService.AddAsync(portfolioId, methodId);
            return Ok(ApiResponse.Success("Method added successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpDelete("{portfolioId:int}/method/{methodId:int}")]
        public async Task<IActionResult> DeleteMethod(int portfolioId, int methodId)
        {
            await _portfolioMethodService.DeleteAsync(portfolioId, methodId);
            return Ok(ApiResponse.Success("Method deleted successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPost("{portfolioId:int}/image/{imageId:int}")]
        public async Task<IActionResult> AddImage(int portfolioId, int imageId, [FromBody] AddPortfolioImageDTO addPortfolioImageDTO)
        {
            await _portfolioImageService.AddAsync(portfolioId, imageId, addPortfolioImageDTO);
            return Ok(ApiResponse.Success("Image added successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpDelete("{portfolioId:int}/image/{imageId:int}")]
        public async Task<IActionResult> DeleteImage(int portfolioId, int imageId)
        {
            await _portfolioImageService.DeleteAsync(portfolioId, imageId);
            return Ok(ApiResponse.Success("Image deleted successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPost("{portfolioId:int}/contributor/{contributorId:int}")]
        public async Task<IActionResult> AddContributor(int portfolioId, int contributorId)
        {
            await _portfolioContributorService.AddAsync(portfolioId, contributorId);
            return Ok(ApiResponse.Success("Contributor added successfully"));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpDelete("{portfolioId:int}/contributor/{contributorId:int}")]
        public async Task<IActionResult> DeleteContributor(int portfolioId, int contributorId)
        {
            await _portfolioContributorService.DeleteAsync(portfolioId, contributorId);
            return Ok(ApiResponse.Success("Contributor deleted successfully"));
        }
    }
}