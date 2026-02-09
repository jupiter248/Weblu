using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Portfolios.PortfolioCategoryDTOs;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Parameters.Portfolios;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Portfolios.PortfolioCategory;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Portfolios
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/portfolio-category")]
    public class PortfolioCategoryController : ControllerBase
    {
        private readonly IPortfolioCategoryService _portfolioCategoryService;
        public PortfolioCategoryController(IPortfolioCategoryService portfolioCategoryService)
        {
            _portfolioCategoryService = portfolioCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PortfolioCategoryParameters portfolioCategoryParameters)
        {
            List<PortfolioCategoryDTO> portfolioCategoryDTOs = await _portfolioCategoryService.GetAllAsync(portfolioCategoryParameters);
            return Ok(portfolioCategoryDTOs);
        }
        [HttpGet("{portfolioCategoryId:int}")]
        public async Task<IActionResult> GetById(int portfolioCategoryId)
        {
            PortfolioCategoryDTO portfolioCategoryDTO = await _portfolioCategoryService.GetByIdAsync(portfolioCategoryId);
            return Ok(portfolioCategoryDTO);
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePortfolioCategoryDTO createPortfolioCategoryDTO)
        {
            Validator.ValidateAndThrow(createPortfolioCategoryDTO, new CreatePortfolioCategoryValidator());
            PortfolioCategoryDTO portfolioCategoryDTO = await _portfolioCategoryService.CreateAsync(createPortfolioCategoryDTO);
            return CreatedAtAction(nameof(GetById), new { portfolioCategoryId = portfolioCategoryDTO.Id }, ApiResponse<PortfolioCategoryDTO>.Success
            (
                "Portfolio Category added successfully.",
                portfolioCategoryDTO
            ));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPut("{portfolioCategoryId:int}")]
        public async Task<IActionResult> Update(int portfolioCategoryId, [FromBody] UpdatePortfolioCategoryDTO updatePortfolioCategoryDTO)
        {
            Validator.ValidateAndThrow(updatePortfolioCategoryDTO, new UpdatePortfolioCategoryValidator());
            PortfolioCategoryDTO portfolioCategoryDTO = await _portfolioCategoryService.UpdateAsync(portfolioCategoryId, updatePortfolioCategoryDTO);
            return Ok(ApiResponse<PortfolioCategoryDTO>.Success
            (
                "Portfolio Category updated successfully.",
                portfolioCategoryDTO
            ));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpDelete("{portfolioCategoryId:int}")]
        public async Task<IActionResult> Delete(int portfolioCategoryId)
        {
            await _portfolioCategoryService.DeleteAsync(portfolioCategoryId);
            return Ok(ApiResponse.Success
            (
                "Portfolio Category deleted successfully."
            ));
        }
    }
}