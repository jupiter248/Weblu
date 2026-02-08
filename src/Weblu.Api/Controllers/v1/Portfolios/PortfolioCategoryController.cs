using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Portfolios.PortfolioCategory;
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
            List<PortfolioCategoryDto> portfolioCategoryDtos = await _portfolioCategoryService.GetAllAsync(portfolioCategoryParameters);
            return Ok(portfolioCategoryDtos);
        }
        [HttpGet("{portfolioCategoryId:int}")]
        public async Task<IActionResult> GetById(int portfolioCategoryId)
        {
            PortfolioCategoryDto portfolioCategoryDto = await _portfolioCategoryService.GetByIdAsync(portfolioCategoryId);
            return Ok(portfolioCategoryDto);
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePortfolioCategoryDto createPortfolioCategoryDto)
        {
            Validator.ValidateAndThrow(createPortfolioCategoryDto, new CreatePortfolioCategoryValidator());
            PortfolioCategoryDto portfolioCategoryDto = await _portfolioCategoryService.CreateAsync(createPortfolioCategoryDto);
            return CreatedAtAction(nameof(GetById), new { portfolioCategoryId = portfolioCategoryDto.Id }, ApiResponse<PortfolioCategoryDto>.Success
            (
                "Portfolio Category added successfully.",
                portfolioCategoryDto
            ));
        }
        [Authorize(Policy = Permissions.ManagePortfolios)]
        [HttpPut("{portfolioCategoryId:int}")]
        public async Task<IActionResult> Update(int portfolioCategoryId, [FromBody] UpdatePortfolioCategoryDto updatePortfolioCategoryDto)
        {
            Validator.ValidateAndThrow(updatePortfolioCategoryDto, new UpdatePortfolioCategoryValidator());
            PortfolioCategoryDto portfolioCategoryDto = await _portfolioCategoryService.UpdateAsync(portfolioCategoryId, updatePortfolioCategoryDto);
            return Ok(ApiResponse<PortfolioCategoryDto>.Success
            (
                "Portfolio Category updated successfully.",
                portfolioCategoryDto
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