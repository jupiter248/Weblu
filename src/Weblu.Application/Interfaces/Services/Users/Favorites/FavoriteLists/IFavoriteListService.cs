using Weblu.Application.DTOs.Users.Favorites.FavoriteListDTOs;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Users;

namespace Weblu.Application.Interfaces.Services.Users.UserFavorites.FavoriteLists
{
    public interface IFavoriteListService
    {
        Task<List<FavoriteListDTO>> GetAllAsync(string userId, FavoriteListParameters favoriteListParameters);
        Task<FavoriteListDTO> GetByIdAsync(int favoriteListId);
        Task<FavoriteListDTO> CreateAsync(string userId, CreateFavoriteListDTO createFavoriteListDTO);
        Task<FavoriteListDTO> UpdateAsync(string userId, int favoriteListId, UpdateFavoriteListDTO updateFavoriteListDTO);
        Task DeleteAsync(string userId, int favoriteListId);
    }
}