using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ArticleDtos;
using Weblu.Application.Dtos.ArticleDtos.ArticleImageDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/article")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllArticles([FromQuery] ArticleParameters articleParameters)
        {
            List<ArticleSummaryDto> articleSummaryDtos = await _articleService.GetAllArticlesAsync(articleParameters);
            return Ok(articleSummaryDtos);
        }
        [HttpGet("{articleId:int}")]
        public async Task<IActionResult> GetArticleById(int articleId)
        {
            ArticleDetailDto articleDetailDto = await _articleService.GetArticleByIdAsync(articleId);
            return Ok(articleDetailDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddArticle([FromBody] AddArticleDto addArticleDto)
        {
            Validator.ValidateAndThrow(addArticleDto, new AddArticleValidator());
            ArticleDetailDto articleDetailDto = await _articleService.AddArticleAsync(addArticleDto);
            return CreatedAtAction(nameof(GetArticleById), new { articleId = articleDetailDto.Id }, ApiResponse<ArticleDetailDto>.Success("Article created successfully", articleDetailDto));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{articleId:int}")]
        public async Task<IActionResult> UpdateArticle(int articleId, [FromBody] UpdateArticleDto updateArticleDto)
        {
            Validator.ValidateAndThrow(updateArticleDto, new UpdateArticleValidator());
            ArticleDetailDto articleDetailDto = await _articleService.UpdateArticleAsync(articleId, updateArticleDto);
            return Ok(
                ApiResponse<ArticleDetailDto>.Success
                (
                    "Article updated successfully",
                    articleDetailDto
                )
            );
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{articleId:int}")]
        public async Task<IActionResult> DeleteArticle(int articleId)
        {
            await _articleService.DeleteArticleAsync(articleId);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{articleId:int}/image/{imageId:int}")]
        public async Task<IActionResult> AddImageToArticle(int articleId, int imageId, [FromBody] AddArticleImageDto addArticleImageDto)
        {
            await _articleService.AddImageToArticleAsync(articleId, imageId, addArticleImageDto);
            return Ok(ApiResponse.Success("Image added successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{articleId:int}/image/{imageId:int}")]
        public async Task<IActionResult> DeleteImageFromArticle(int articleId, int imageId)
        {
            await _articleService.DeleteImageFromArticleAsync(articleId, imageId);
            return Ok(ApiResponse.Success("Image deleted successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{articleId:int}/contributor/{contributorId:int}")]
        public async Task<IActionResult> AddContributorToArticle(int articleId, int contributorId)
        {
            await _articleService.AddContributorToArticleAsync(articleId, contributorId);
            return Ok(ApiResponse.Success("Contributor added successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{articleId:int}/contributor/{contributorId:int}")]
        public async Task<IActionResult> DeleteContributorFromArticle(int articleId, int contributorId)
        {
            await _articleService.DeleteContributorFromArticleAsync(articleId, contributorId);
            return Ok(ApiResponse.Success("Contributor deleted successfully"));
        }
        [Authorize]
        [HttpPost("{articleId:int}/like")]
        public async Task<IActionResult> LikeArticle(int articleId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _articleService.LikeArticleAsync(articleId, userId);
            return Ok(ApiResponse.Success("User liked the article successfully"));
        }
        [Authorize]
        [HttpDelete("{articleId:int}/unlike")]
        public async Task<IActionResult> UnlikeArticle(int articleId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _articleService.UnlikeArticleAsync(articleId, userId);
            return Ok(ApiResponse.Success("User unLiked the article successfully"));
        }
        [Authorize]
        [EnableRateLimiting("ArticleViewPolicy")]
        [HttpPost("{articleId:int}/View")]
        public async Task<IActionResult> ViewArticle(int articleId)
        {
            await _articleService.ViewArticleAsync(articleId);
            return Ok(ApiResponse.Success("User Viewed the article successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{articleId:int}/tag/{tagId:int}")]
        public async Task<IActionResult> AddTagToArticle(int articleId, int tagId)
        {
            await _articleService.AddTagToArticleAsync(articleId, tagId);
            return Ok(ApiResponse.Success("Tag added successfully"));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{articleId:int}/tag/{tagId:int}")]
        public async Task<IActionResult> DeleteTagFromArticle(int articleId, int tagId)
        {
            await _articleService.DeleteTagFromArticleAsync(articleId, tagId);
            return Ok(ApiResponse.Success("Tag deleted successfully"));
        }
    }
}