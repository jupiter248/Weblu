using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Articles.ArticleDTOs;
using Weblu.Application.DTOs.Articles.ArticleDTOs.ArticleImageDTOs;
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
            List<ArticleSummaryDTO> articleSummaryDTOs = await _articleService.GetAllAsync(articleParameters);
            return Ok(articleSummaryDTOs);
        }
        [HttpGet("{articleId:int}")]
        public async Task<IActionResult> GetById(int articleId)
        {
            ArticleDetailDTO articleDetailDTO = await _articleService.GetByIdAsync(articleId);
            return Ok(articleDetailDTO);
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticleDTO createArticleDTO)
        {
            Validator.ValidateAndThrow(createArticleDTO, new CreateArticleValidator());
            ArticleDetailDTO articleDetailDTO = await _articleService.CreateAsync(createArticleDTO);
            return CreatedAtAction(nameof(GetById), new { articleId = articleDetailDTO.Id }, ApiResponse<ArticleDetailDTO>.Success("Article created successfully", articleDetailDTO));
        }
        [Authorize(Policy = Permissions.ManageArticles)]
        [HttpPut("{articleId:int}")]
        public async Task<IActionResult> Edit(int articleId, [FromBody] UpdateArticleDTO updateArticleDTO)
        {
            Validator.ValidateAndThrow(updateArticleDTO, new UpdateArticleValidator());
            ArticleDetailDTO articleDetailDTO = await _articleService.EditAsync(articleId, updateArticleDTO);
            return Ok(
                ApiResponse<ArticleDetailDTO>.Success
                (
                    "Article updated successfully",
                    articleDetailDTO
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
        public async Task<IActionResult> AddImage(int articleId, int imageId, [FromBody] AddArticleImageDTO addArticleImageDTO)
        {
            await _articleImageService.AddAsync(articleId, imageId, addArticleImageDTO);
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