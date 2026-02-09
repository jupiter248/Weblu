using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Users.Favorites.FavoriteListDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services.Users.Favorites.FavoriteLists;
using Weblu.Application.Interfaces.Services.Users.UserFavorites.FavoriteLists;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers.v1.Users.Favorites
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/favorite-list")]
    public class FavoriteListController : ControllerBase
    {
        private readonly IFavoriteListService _favoriteListService;
        private readonly IFavoriteListPortfolioService _favoriteListPortfolioService;
        private readonly IFavoriteListArticleService _favoriteListArticleService;
        public FavoriteListController(IFavoriteListService favoriteListService, IFavoriteListPortfolioService favoriteListPortfolioService, IFavoriteListArticleService favoriteListArticleService)
        {
            _favoriteListService = favoriteListService;
            _favoriteListPortfolioService = favoriteListPortfolioService;
            _favoriteListArticleService = favoriteListArticleService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FavoriteListParameters favoriteListParameters)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            List<FavoriteListDto> favoriteListDtos = await _favoriteListService.GetAllAsync(userId, favoriteListParameters);
            return Ok(favoriteListDtos);
        }
        [Authorize]
        [HttpGet("{favoriteListId:int}")]
        public async Task<IActionResult> GetById(int favoriteListId)
        {
            FavoriteListDto favoriteListDto = await _favoriteListService.GetByIdAsync(favoriteListId);
            return Ok(favoriteListDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFavoriteListDto createFavoriteListDto)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteListDto favoriteListDto = await _favoriteListService.CreateAsync(userId, createFavoriteListDto);
            return CreatedAtAction(nameof(GetById), new { favoriteListId = favoriteListDto.Id }, ApiResponse<FavoriteListDto>.Success("Favorite list created successfully", favoriteListDto));
        }
        [Authorize]
        [HttpPut("{favoriteListId:int}")]
        public async Task<IActionResult> Update(int favoriteListId, [FromBody] UpdateFavoriteListDto updateFavoriteListDto)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteListDto favoriteListDto = await _favoriteListService.UpdateAsync(userId, favoriteListId, updateFavoriteListDto);
            return Ok(ApiResponse<FavoriteListDto>.Success(
                "Favorite list updated successfully.",
                favoriteListDto
            ));
        }
        [Authorize]
        [HttpDelete("{favoriteListId:int}")]
        public async Task<IActionResult> Delete(int favoriteListId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _favoriteListService.DeleteAsync(userId, favoriteListId);
            return Ok(ApiResponse.Success(
                "Favorite list deleted successfully."
            ));
        }
        [Authorize]
        [HttpPost("{favoriteListId:int}/portfolio/{portfolioId:int}")]
        public async Task<IActionResult> AddPortfolio(int favoriteListId, int portfolioId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _favoriteListPortfolioService.AddAsync(userId, favoriteListId, portfolioId);
            return Ok(ApiResponse.Success(
                "Portfolio added to Favorite list successfully."
            ));
        }
        [Authorize]
        [HttpDelete("{favoriteListId:int}/portfolio/{portfolioId:int}")]
        public async Task<IActionResult> DeletePortfolio(int favoriteListId, int portfolioId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _favoriteListPortfolioService.DeleteAsync(userId, favoriteListId, portfolioId);
            return Ok(ApiResponse.Success(
                "Portfolio deleted to Favorite list successfully."
            ));
        }
        [Authorize]
        [HttpPost("{favoriteListId:int}/article/{articleId:int}")]
        public async Task<IActionResult> AddArticleTo(int favoriteListId, int articleId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _favoriteListArticleService.AddAsync(userId, favoriteListId, articleId);
            return Ok(ApiResponse.Success(
                "Article added to Favorite list successfully."
            ));
        }
        [Authorize]
        [HttpDelete("{favoriteListId:int}/article/{articleId:int}")]
        public async Task<IActionResult> DeleteArticle(int favoriteListId, int articleId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _favoriteListArticleService.DeleteAsync(userId, favoriteListId, articleId);
            return Ok(ApiResponse.Success(
                "Article deleted to Favorite list successfully."
            ));
        }
    }
}