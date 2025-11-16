using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.RateLimiting;
using Weblu.Application.Dtos.FavoriteListDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services
{
    public interface IFavoriteListService
    {
        Task<List<FavoriteListDto>> GetAllFavoriteListsAsync(string userId, FavoriteListParameters favoriteListParameters);
        Task<FavoriteListDto> GetFavoriteListByIdAsync(int favoriteListId);
        Task<FavoriteListDto> AddFavoriteListAsync(string userId, AddFavoriteListDto addFavoriteListDto);
        Task<FavoriteListDto> UpdateFavoriteListAsync(string userId, int favoriteListId, UpdateFavoriteListDto updateFavoriteListDto);
        Task DeleteFavoriteListAsync(string userId, int favoriteListId);
        Task AddPortfolioToFavoriteListAsync(string userId, int favoriteListId, int favoritePortfolioId);
        Task DeletePortfolioFromFavoriteListAsync(string userId, int favoriteListId, int favoritePortfolioId);


    }
}