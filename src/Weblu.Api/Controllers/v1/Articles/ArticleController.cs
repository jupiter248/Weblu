using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Articles.ArticleDtos;
using Weblu.Application.Dtos.Articles.ArticleDtos.ArticleImageDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Articles.ArticleValidations;
using Weblu.Domain.Errors.Users;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Articles
{
    [ApiController]
    [Route("api/article")]
    [ApiVersion("1")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IArticleContributorService _articleContributorService;
        private readonly IArticleTagService _articleTagService;
        private readonly IArticleImageService _articleImageService;
        private readonly IArticleLikeService _articleLikeService;

        public ArticleController(
            IArticleService articleService,
            IArticleTagService articleTagService,
            IArticleImageService articleImageService,
            IArticleLikeService articleLikeService,
            IArticleContributorService articleContributorService)
        {
            _articleService = articleService;
            _articleContributorService = articleContributorService;
            _articleImageService = articleImageService;
            _articleLikeService = articleLikeService;
            _articleTagService = articleTagService;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ArticleParameters articleParameters)
        {
            List<ArticleSummaryDto> articleSummaryDtos = await _articleService.GetAllAsync(articleParameters);
            return Ok(articleSummaryDtos);
        }
        [HttpGet("{articleId:int}")]
        public async Task<IActionResult> GetById(int articleId)
        {
            ArticleDetailDto articleDetailDto = await _articleService.GetByIdAsync(articleId);
            return Ok(articleDetailDto);
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticleDto createArticleDto)
        {
            Validator.ValidateAndThrow(createArticleDto, new CreateArticleValidator());
            ArticleDetailDto articleDetailDto = await _articleService.CreateAsync(createArticleDto);
            return CreatedAtAction(nameof(GetById), new { articleId = articleDetailDto.Id }, ApiResponse<ArticleDetailDto>.Success("Article created successfully", articleDetailDto));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPut("{articleId:int}")]
        public async Task<IActionResult> Edit(int articleId, [FromBody] UpdateArticleDto updateArticleDto)
        {
            Validator.ValidateAndThrow(updateArticleDto, new UpdateArticleValidator());
            ArticleDetailDto articleDetailDto = await _articleService.EditAsync(articleId, updateArticleDto);
            return Ok(
                ApiResponse<ArticleDetailDto>.Success
                (
                    "Article updated successfully",
                    articleDetailDto
                )
            );
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpDelete("{articleId:int}")]
        public async Task<IActionResult> Delete(int articleId)
        {
            await _articleService.DeleteAsync(articleId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPut("{articleId:int}/publish")]
        public async Task<IActionResult> Publish(int articleId)
        {
            await _articleService.Publish(articleId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPut("{articleId:int}/unpublish")]
        public async Task<IActionResult> Unpublish(int articleId)
        {
            await _articleService.Unpublish(articleId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPost("{articleId:int}/image/{imageId:int}")]
        public async Task<IActionResult> AddImage(int articleId, int imageId, [FromBody] AddArticleImageDto addArticleImageDto)
        {
            await _articleImageService.AddAsync(articleId, imageId, addArticleImageDto);
            return Ok(ApiResponse.Success("Image added successfully"));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpDelete("{articleId:int}/image/{imageId:int}")]
        public async Task<IActionResult> DeleteImage(int articleId, int imageId)
        {
            await _articleImageService.DeleteAsync(articleId, imageId);
            return Ok(ApiResponse.Success("Image deleted successfully"));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPost("{articleId:int}/contributor/{contributorId:int}")]
        public async Task<IActionResult> AddContributor(int articleId, int contributorId)
        {
            await _articleContributorService.AddAsync(articleId, contributorId);
            return Ok(ApiResponse.Success("Contributor added successfully"));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpDelete("{articleId:int}/contributor/{contributorId:int}")]
        public async Task<IActionResult> DeleteContributor(int articleId, int contributorId)
        {
            await _articleContributorService.DeleteAsync(articleId, contributorId);
            return Ok(ApiResponse.Success("Contributor deleted successfully"));
        }
        [Authorize]
        [HttpPost("{articleId:int}/like")]
        public async Task<IActionResult> Like(int articleId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _articleLikeService.LikeAsync(articleId, userId);
            return Ok(ApiResponse.Success("User liked the article successfully"));
        }
        [Authorize]
        [HttpDelete("{articleId:int}/unlike")]
        public async Task<IActionResult> Unlike(int articleId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _articleLikeService.UnlikeAsync(articleId, userId);
            return Ok(ApiResponse.Success("User unLiked the article successfully"));
        }
        [Authorize]
        [EnableRateLimiting("ArticleViewPolicy")]
        [HttpPost("{articleId:int}/View")]
        public async Task<IActionResult> View(int articleId)
        {
            await _articleService.ViewAsync(articleId);
            return Ok(ApiResponse.Success("User Viewed the article successfully"));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPost("{articleId:int}/tag/{tagId:int}")]
        public async Task<IActionResult> AddTag(int articleId, int tagId)
        {
            await _articleTagService.AddAsync(articleId, tagId);
            return Ok(ApiResponse.Success("Tag added successfully"));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpDelete("{articleId:int}/tag/{tagId:int}")]
        public async Task<IActionResult> DeleteTag(int articleId, int tagId)
        {
            await _articleTagService.DeleteAsync(articleId, tagId);
            return Ok(ApiResponse.Success("Tag deleted successfully"));
        }
    }
}