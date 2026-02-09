using Weblu.Application.Dtos.Users.Favorites.FavoriteListDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Users;

namespace Weblu.Application.Interfaces.Services.Users.UserFavorites.FavoriteLists
{
    public interface IFavoriteListService
    {
        Task<List<FavoriteListDto>> GetAllAsync(string userId, FavoriteListParameters favoriteListParameters);
        Task<FavoriteListDto> GetByIdAsync(int favoriteListId);
        Task<FavoriteListDto> CreateAsync(string userId, CreateFavoriteListDto createFavoriteListDto);
        Task<FavoriteListDto> UpdateAsync(string userId, int favoriteListId, UpdateFavoriteListDto updateFavoriteListDto);
        Task DeleteAsync(string userId, int favoriteListId);
    }
}