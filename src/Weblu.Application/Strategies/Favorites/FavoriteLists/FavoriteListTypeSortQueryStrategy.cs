using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;
using Weblu.Domain.Enums.Users.Favorites;
using Weblu.Domain.Enums.Users.Favorites.Parameters;

namespace Weblu.Application.Strategies.Favorites.FavoriteLists
{
    public class FavoriteListTypeSortQueryStrategy : IFavoriteListQueryStrategy
    {
        public IQueryable<FavoriteList> Query(IQueryable<FavoriteList> favoriteLists, FavoriteListParameters favoriteListParameters)
        {
            if (favoriteListParameters.FavoriteListTypeSort == FavoriteListTypeSort.Portfolio)
            {
                return favoriteLists.Where(t => t.FavoriteListType == FavoriteListType.Portfolio);
            }
            else if (favoriteListParameters.FavoriteListTypeSort == FavoriteListTypeSort.Article)
            {
                return favoriteLists.Where(t => t.FavoriteListType == FavoriteListType.Article);
            }
            else
            {
                return favoriteLists;
            }
        }
    }
}