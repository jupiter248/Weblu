using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services.Users.UserFavorites;
using Weblu.Application.Parameters;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/favorite/portfolio")]
    public class UserPortfolioFavoriteController : ControllerBase
    {
        private readonly IUserPortfolioFavoriteService _userPortfolioFavoriteService;
        public UserPortfolioFavoriteController(IUserPortfolioFavoriteService userPortfolioFavoriteService)
        {
            _userPortfolioFavoriteService = userPortfolioFavoriteService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllFavoritePortfolios([FromQuery] FavoriteParameters favoriteParameters)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            List<PortfolioSummaryDto> portfolioSummaryDtos = await _userPortfolioFavoriteService.GetAllAsync(userId, favoriteParameters);
            return Ok(portfolioSummaryDtos);
        }
        [Authorize]
        [HttpGet("{portfolioId:int}/status")]
        public async Task<IActionResult> IsFavorite(int portfolioId)
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
        [HttpPost("{portfolioId:int}")]
        public async Task<IActionResult> AddPortfolioToFavorite(int portfolioId)
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
        [HttpDelete("{portfolioId:int}")]
        public async Task<IActionResult> DeletePortfolioFromFavorite(int portfolioId)
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