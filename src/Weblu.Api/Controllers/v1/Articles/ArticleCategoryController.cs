using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Articles.ArticleCategoryDtos;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Articles.ArticleCategories;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Articles
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/article-category")]
    public class ArticleCategoryController : ControllerBase
    {
        private readonly IArticleCategoryService _articleCategoryService;
        public ArticleCategoryController(IArticleCategoryService articleCategoryService)
        {
            _articleCategoryService = articleCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ArticleCategoryParameters articleCategoryParameters)
        {
            List<ArticleCategoryDto> articleCategoryDtos = await _articleCategoryService.GetAllAsync(articleCategoryParameters);
            return Ok(articleCategoryDtos);
        }
        [HttpGet("{articleCategoryId:int}")]
        public async Task<IActionResult> GetById(int articleCategoryId)
        {
            ArticleCategoryDto articleCategoryDto = await _articleCategoryService.GetByIdAsync(articleCategoryId);
            return Ok(articleCategoryDto);
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticleCategoryDto createArticleCategoryDto)
        {
            Validator.ValidateAndThrow(createArticleCategoryDto, new CreateArticleCategoryValidator());
            ArticleCategoryDto articleCategoryDto = await _articleCategoryService.CreateAsync(createArticleCategoryDto);
            return CreatedAtAction(nameof(GetById), new { articleCategoryId = articleCategoryDto.Id }, ApiResponse<ArticleCategoryDto>.Success
            (
                "Article Category added successfully.",
                articleCategoryDto
            ));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPut("{articleCategoryId:int}")]
        public async Task<IActionResult> Update(int articleCategoryId, [FromBody] UpdateArticleCategoryDto updateArticleCategoryDto)
        {
            Validator.ValidateAndThrow(updateArticleCategoryDto, new UpdateArticleCategoryValidator());
            ArticleCategoryDto articleCategoryDto = await _articleCategoryService.UpdateAsync(articleCategoryId, updateArticleCategoryDto);
            return Ok(ApiResponse<ArticleCategoryDto>.Success
            (
                "Article Category updated successfully.",
                articleCategoryDto
            ));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpDelete("{articleCategoryId:int}")]
        public async Task<IActionResult> Delete(int articleCategoryId)
        {
            await _articleCategoryService.DeleteAsync(articleCategoryId);
            return Ok(ApiResponse.Success
            (
                "Article Category deleted successfully."
            ));
        }
    }
}