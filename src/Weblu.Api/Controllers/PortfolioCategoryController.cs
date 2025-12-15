using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.PortfolioCategory;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Parameters;
using Weblu.Application.Validations;
using Weblu.Application.Validations.PortfolioCategory;

namespace Weblu.Api.Controllers
{
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
        public async Task<IActionResult> GetAllPortfolioCategories([FromQuery] PortfolioCategoryParameters portfolioCategoryParameters)
        {
            List<PortfolioCategoryDto> portfolioCategoryDtos = await _portfolioCategoryService.GetAllPortfolioCategoriesAsync(portfolioCategoryParameters);
            return Ok(portfolioCategoryDtos);
        }
        [HttpGet("{portfolioCategoryId:int}")]
        public async Task<IActionResult> GetArticleCategoryById(int portfolioCategoryId)
        {
            PortfolioCategoryDto portfolioCategoryDto = await _portfolioCategoryService.GetPortfolioCategoryByIdAsync(portfolioCategoryId);
            return Ok(portfolioCategoryDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddPortfolioCategory([FromBody] AddPortfolioCategoryDto addPortfolioCategoryDto)
        {
            Validator.ValidateAndThrow(addPortfolioCategoryDto, new AddPortfolioCategoryValidator());
            PortfolioCategoryDto portfolioCategoryDto = await _portfolioCategoryService.AddPortfolioCategoryAsync(addPortfolioCategoryDto);
            return CreatedAtAction(nameof(GetArticleCategoryById), new { portfolioCategoryId = portfolioCategoryDto.Id }, ApiResponse<PortfolioCategoryDto>.Success
            (
                "Portfolio Category added successfully.",
                portfolioCategoryDto
            ));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{portfolioCategoryId:int}")]
        public async Task<IActionResult> UpdatePortfolioCategory(int portfolioCategoryId, [FromBody] UpdatePortfolioCategoryDto updatePortfolioCategoryDto)
        {
            Validator.ValidateAndThrow(updatePortfolioCategoryDto, new UpdatePortfolioCategoryValidator());
            PortfolioCategoryDto portfolioCategoryDto = await _portfolioCategoryService.UpdatePortfolioCategoryAsync(portfolioCategoryId, updatePortfolioCategoryDto);
            return Ok(ApiResponse<PortfolioCategoryDto>.Success
            (
                "Portfolio Category updated successfully.",
                portfolioCategoryDto
            ));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{portfolioCategoryId:int}")]
        public async Task<IActionResult> DeletePortfolioCategory(int portfolioCategoryId)
        {
            await _portfolioCategoryService.DeletePortfolioCategoryAsync(portfolioCategoryId);
            return Ok(ApiResponse.Success
            (
                "Portfolio Category deleted successfully."
            ));
        }
    }
}