using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Articles.ArticleCategoryDTOs;
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
            List<ArticleCategoryDTO> articleCategoryDTOs = await _articleCategoryService.GetAllAsync(articleCategoryParameters);
            return Ok(articleCategoryDTOs);
        }
        [HttpGet("{articleCategoryId:int}")]
        public async Task<IActionResult> GetById(int articleCategoryId)
        {
            ArticleCategoryDTO articleCategoryDTO = await _articleCategoryService.GetByIdAsync(articleCategoryId);
            return Ok(articleCategoryDTO);
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticleCategoryDTO createArticleCategoryDTO)
        {
            Validator.ValidateAndThrow(createArticleCategoryDTO, new CreateArticleCategoryValidator());
            ArticleCategoryDTO articleCategoryDTO = await _articleCategoryService.CreateAsync(createArticleCategoryDTO);
            return CreatedAtAction(nameof(GetById), new { articleCategoryId = articleCategoryDTO.Id }, ApiResponse<ArticleCategoryDTO>.Success
            (
                "Article Category added successfully.",
                articleCategoryDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPut("{articleCategoryId:int}")]
        public async Task<IActionResult> Update(int articleCategoryId, [FromBody] UpdateArticleCategoryDTO updateArticleCategoryDTO)
        {
            Validator.ValidateAndThrow(updateArticleCategoryDTO, new UpdateArticleCategoryValidator());
            ArticleCategoryDTO articleCategoryDTO = await _articleCategoryService.UpdateAsync(articleCategoryId, updateArticleCategoryDTO);
            return Ok(ApiResponse<ArticleCategoryDTO>.Success
            (
                "Article Category updated successfully.",
                articleCategoryDTO
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