using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.FavoriteListDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/favorite-list")]
    public class FavoriteListController : ControllerBase
    {
        private readonly IFavoriteListService _favoriteListService;
        public FavoriteListController(IFavoriteListService favoriteListService)
        {
            _favoriteListService = favoriteListService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllFavoriteLists([FromQuery] FavoriteListParameters favoriteListParameters)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            List<FavoriteListDto> favoriteListDtos = await _favoriteListService.GetAllFavoriteListsAsync(userId, favoriteListParameters);
            return Ok(favoriteListDtos);
        }
        [Authorize]
        [HttpGet("{favoriteListId:int}")]
        public async Task<IActionResult> GetFavoriteListById(int favoriteListId)
        {
            FavoriteListDto favoriteListDto = await _favoriteListService.GetFavoriteListByIdAsync(favoriteListId);
            return Ok(favoriteListDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateFavoriteList([FromBody] AddFavoriteListDto addFavoriteListDto)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteListDto favoriteListDto = await _favoriteListService.AddFavoriteListAsync(userId, addFavoriteListDto);
            return CreatedAtAction(nameof(GetFavoriteListById), new { favoriteListId = favoriteListDto.Id }, ApiResponse<FavoriteListDto>.Success("Favorite list created successfully", favoriteListDto));
        }
        [Authorize]
        [HttpPut("{favoriteListId:int}")]
        public async Task<IActionResult> UpdateFavoriteList(int favoriteListId, [FromBody] UpdateFavoriteListDto updateFavoriteListDto)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteListDto favoriteListDto = await _favoriteListService.UpdateFavoriteListAsync(userId, favoriteListId, updateFavoriteListDto);
            return Ok(ApiResponse<FavoriteListDto>.Success(
                "Favorite list updated successfully.",
                favoriteListDto
            ));
        }
        [Authorize]
        [HttpDelete("{favoriteListId:int}")]
        public async Task<IActionResult> DeleteFavoriteList(int favoriteListId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _favoriteListService.DeleteFavoriteListAsync(userId, favoriteListId);
            return Ok(ApiResponse.Success(
                "Favorite list deleted successfully."
            ));
        }
        [Authorize]
        [HttpPost("{favoriteListId:int}/portfolio/{portfolioId:int}")]
        public async Task<IActionResult> AddPortfolioToFavoriteList(int favoriteListId, int portfolioId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _favoriteListService.AddPortfolioToFavoriteListAsync(userId, favoriteListId, portfolioId);
            return Ok(ApiResponse.Success(
                "Portfolio added to Favorite list successfully."
            ));
        }
        [Authorize]
        [HttpDelete("{favoriteListId:int}/portfolio/{portfolioId:int}")]
        public async Task<IActionResult> DeletePortfolioFromFavoriteList(int favoriteListId, int portfolioId)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _favoriteListService.DeletePortfolioFromFavoriteListAsync(userId, favoriteListId, portfolioId);
            return Ok(ApiResponse.Success(
                "Portfolio deleted to Favorite list successfully."
            ));
        }
    }
}