using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ArticleDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services.Users.UserFavorites;
using Weblu.Application.Parameters;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/favorite/article")]
    public class UserArticleFavoriteController : ControllerBase
    {
        private readonly IUserArticleFavoriteService _articleFavoriteService;
        public UserArticleFavoriteController(IUserArticleFavoriteService articleFavoriteService)
        {
            _articleFavoriteService = articleFavoriteService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllFavoriteArticles([FromQuery] FavoriteParameters favoriteParameters)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            List<ArticleSummaryDto> articleSummaryDtos = await _articleFavoriteService.GetAllAsync(userId, favoriteParameters);
            return Ok(articleSummaryDtos);
        }
        [Authorize]
        [HttpGet("{articleId:int}/status")]
        public async Task<IActionResult> IsFavorite(int articleId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            bool isFavorite = await _articleFavoriteService.IsFavoriteAsync(userId, articleId);
            return Ok(new
            {
                isFavorite
            });
        }
        [Authorize]
        [HttpPost("{articleId:int}")]
        public async Task<IActionResult> AddArticleToFavorite(int articleId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _articleFavoriteService.AddAsync(userId, articleId);
            return Ok(ApiResponse.Success
            (
                "Article added to favorites successfully."
            ));
        }
        [Authorize]
        [HttpDelete("{articleId:int}")]
        public async Task<IActionResult> DeleteArticleFromFavorite(int articleId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _articleFavoriteService.DeleteAsync(userId, articleId);
            return Ok(ApiResponse.Success
            (
                "Article deleted from favorites successfully."
            ));
        }
    }
}