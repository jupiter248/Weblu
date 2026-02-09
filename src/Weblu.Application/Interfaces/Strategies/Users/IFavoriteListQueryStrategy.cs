using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Interfaces.Strategies.Users
{
    public interface IFavoriteListQueryStrategy
    {
        IQueryable<FavoriteList> Query(IQueryable<FavoriteList> favoriteLists, FavoriteListParameters favoriteListParameters);
    }
}