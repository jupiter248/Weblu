using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.FavoriteDtos;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/favorite")]
    public class UserFavoriteController : ControllerBase
    {
        private readonly IUserFavoriteService _userFavoriteService;
        public UserFavoriteController(IUserFavoriteService userFavoriteService)
        {
            _userFavoriteService = userFavoriteService;
        }
        [HttpGet("portfolio")]
        public async Task<IActionResult> GetAllFavoritePortfolios([FromQuery] FavoriteParameters favoriteParameters)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            List<PortfolioSummaryDto> portfolioSummaryDtos = await _userFavoriteService.GetAllFavoritePortfoliosAsync(userId, favoriteParameters);
            return Ok(portfolioSummaryDtos);
        }
        [HttpGet("portfolio/{portfolioId:int}/status")]
        public async Task<IActionResult> IsFavorite(int portfolioId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            bool isFavorite = await _userFavoriteService.IsFavorite(userId, portfolioId);
            return Ok(new
            {
                isFavorite
            });
        }
        [HttpPost("portfolio/{portfolioId:int}")]
        public async Task<IActionResult> AddPortfolioToFavorite(int portfolioId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _userFavoriteService.AddPortfolioToFavorite(userId, portfolioId);
            return Ok(ApiResponse.Success
            (
                "Portfolio added to favorites successfully."
            ));
        }
        [HttpDelete("portfolio/{portfolioId:int}")]
        public async Task<IActionResult> DeletePortfolioFromFavorite(int portfolioId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _userFavoriteService.DeletePortfolioFromFavorite(userId, portfolioId);
            return Ok(ApiResponse.Success
            (
                "Portfolio deleted from favorites successfully."
            ));
        }
    }
}