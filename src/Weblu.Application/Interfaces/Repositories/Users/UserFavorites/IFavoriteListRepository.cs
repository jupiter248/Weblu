using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Interfaces.Repositories.Users.UserFavorites
{
    public interface IFavoriteListRepository : IGenericRepository<FavoriteList, FavoriteListParameters>
    {
        Task<IReadOnlyList<FavoriteList>> GetAllByUserIdAsync(string userId, FavoriteListParameters favoriteListParameters);
        Task<FavoriteList?> GetByUserAndListIdAsync(string userId, int favoriteListId);

    }
}