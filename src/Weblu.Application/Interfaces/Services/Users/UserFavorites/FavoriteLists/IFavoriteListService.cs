using Weblu.Application.Dtos.Users.Favorites.FavoriteListDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Users;

namespace Weblu.Application.Interfaces.Services.Users.UserFavorites.FavoriteLists
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