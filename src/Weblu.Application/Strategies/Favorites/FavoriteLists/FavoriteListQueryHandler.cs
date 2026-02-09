using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Strategies.Favorites.FavoriteLists
{
    public class FavoriteListQueryHandler
    {
        private readonly IFavoriteListQueryStrategy _favoriteListQueryStrategy;
        public FavoriteListQueryHandler(IFavoriteListQueryStrategy favoriteListQueryStrategy)
        {
            _favoriteListQueryStrategy = favoriteListQueryStrategy;
        }

        public IQueryable<FavoriteList> ExecuteFavoriteListQuery(IQueryable<FavoriteList> favoriteLists, FavoriteListParameters favoriteListParameters)
        {
            return _favoriteListQueryStrategy.Query(favoriteLists, favoriteListParameters);
        }
    }
}