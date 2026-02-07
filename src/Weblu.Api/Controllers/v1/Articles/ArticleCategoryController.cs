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
        public async Task<IActionResult> GetAllArticleCategories([FromQuery] ArticleCategoryParameters articleCategoryParameters)
        {
            List<ArticleCategoryDto> articleCategoryDtos = await _articleCategoryService.GetAllArticleCategoriesAsync(articleCategoryParameters);
            return Ok(articleCategoryDtos);
        }
        [HttpGet("{articleCategoryId:int}")]
        public async Task<IActionResult> GetArticleCategoryById(int articleCategoryId)
        {
            ArticleCategoryDto articleCategoryDto = await _articleCategoryService.GetArticleCategoryByIdAsync(articleCategoryId);
            return Ok(articleCategoryDto);
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPost]
        public async Task<IActionResult> AddArticleCategory([FromBody] AddArticleCategoryDto addArticleCategoryDto)
        {
            Validator.ValidateAndThrow(addArticleCategoryDto, new AddArticleCategoryValidator());
            ArticleCategoryDto articleCategoryDto = await _articleCategoryService.AddArticleCategoryAsync(addArticleCategoryDto);
            return CreatedAtAction(nameof(GetArticleCategoryById), new { articleCategoryId = articleCategoryDto.Id }, ApiResponse<ArticleCategoryDto>.Success
            (
                "Article Category added successfully.",
                articleCategoryDto
            ));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPut("{articleCategoryId:int}")]
        public async Task<IActionResult> UpdateArticleCategory(int articleCategoryId, [FromBody] UpdateArticleCategoryDto updateArticleCategoryDto)
        {
            Validator.ValidateAndThrow(updateArticleCategoryDto, new UpdateArticleCategoryValidator());
            ArticleCategoryDto articleCategoryDto = await _articleCategoryService.UpdateArticleCategoryAsync(articleCategoryId, updateArticleCategoryDto);
            return Ok(ApiResponse<ArticleCategoryDto>.Success
            (
                "Article Category updated successfully.",
                articleCategoryDto
            ));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpDelete("{articleCategoryId:int}")]
        public async Task<IActionResult> DeleteArticleCategory(int articleCategoryId)
        {
            await _articleCategoryService.DeleteArticleCategoryAsync(articleCategoryId);
            return Ok(ApiResponse.Success
            (
                "Article Category deleted successfully."
            ));
        }
    }
}