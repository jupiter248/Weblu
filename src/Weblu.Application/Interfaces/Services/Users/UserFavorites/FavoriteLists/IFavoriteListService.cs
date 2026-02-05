using Weblu.Application.Dtos.FavoriteListDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services.Users.UserFavorites
{
    public interface IFavoriteListService
    {
        Task<List<FavoriteListDto>> GetAllFavoriteListsAsync(string userId, FavoriteListParameters favoriteListParameters);
        Task<FavoriteListDto> GetFavoriteListByIdAsync(int favoriteListId);
        Task<FavoriteListDto> AddFavoriteListAsync(string userId, AddFavoriteListDto addFavoriteListDto);
        Task<FavoriteListDto> UpdateFavoriteListAsync(string userId, int favoriteListId, UpdateFavoriteListDto updateFavoriteListDto);
        Task DeleteFavoriteListAsync(string userId, int favoriteListId);
    }
}