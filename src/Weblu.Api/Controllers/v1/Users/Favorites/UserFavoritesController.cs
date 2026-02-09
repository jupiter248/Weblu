using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Articles.ArticleDTOs;
using Weblu.Application.DTOs.Portfolios.PortfolioDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services.Users.Favorites;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers.v1.Users.Favorites
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/favorite")]
    public class UserFavoriteController : ControllerBase
    {
        private readonly IUserArticleFavoriteService _articleFavoriteService;
        private readonly IUserPortfolioFavoriteService _userPortfolioFavoriteService;
        public UserFavoriteController(IUserArticleFavoriteService articleFavoriteService, IUserPortfolioFavoriteService userPortfolioFavoriteService)
        {
            _articleFavoriteService = articleFavoriteService;
            _userPortfolioFavoriteService = userPortfolioFavoriteService;
        }
        [Authorize]
        [HttpGet("article")]
        public async Task<IActionResult> GetAllArticles([FromQuery] FavoriteParameters favoriteParameters)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            List<ArticleSummaryDTO> articleSummaryDTOs = await _articleFavoriteService.GetAllAsync(userId, favoriteParameters);
            return Ok(articleSummaryDTOs);
        }
        [Authorize]
        [HttpGet("article/{articleId:int}/status")]
        public async Task<IActionResult> IsArticleFavorite(int articleId)
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
        [HttpPost("article/{articleId:int}")]
        public async Task<IActionResult> AddArticle(int articleId)
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
        [HttpDelete("article/{articleId:int}")]
        public async Task<IActionResult> DeleteArticle(int articleId)
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
        [Authorize]
        [HttpGet("portfolio")]
        public async Task<IActionResult> GetAllPortfolios([FromQuery] FavoriteParameters favoriteParameters)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            List<PortfolioSummaryDTO> portfolioSummaryDTOs = await _userPortfolioFavoriteService.GetAllAsync(userId, favoriteParameters);
            return Ok(portfolioSummaryDTOs);
        }
        [Authorize]
        [HttpGet("portfolio/{portfolioId:int}/status")]
        public async Task<IActionResult> IsPortfolioFavorite(int portfolioId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            bool isFavorite = await _userPortfolioFavoriteService.IsFavoriteAsync(userId, portfolioId);
            return Ok(new
            {
                isFavorite
            });
        }
        [Authorize]
        [HttpPost("portfolio/{portfolioId:int}")]
        public async Task<IActionResult> AddPortfolio(int portfolioId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _userPortfolioFavoriteService.AddAsync(userId, portfolioId);
            return Ok(ApiResponse.Success
            (
                "Portfolio added to favorites successfully."
            ));
        }
        [Authorize]
        [HttpDelete("portfolio/{portfolioId:int}")]
        public async Task<IActionResult> DeletePortfolio(int portfolioId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _userPortfolioFavoriteService.DeleteAsync(userId, portfolioId);
            return Ok(ApiResponse.Success
            (
                "Portfolio deleted from favorites successfully."
            ));
        }
    }
}