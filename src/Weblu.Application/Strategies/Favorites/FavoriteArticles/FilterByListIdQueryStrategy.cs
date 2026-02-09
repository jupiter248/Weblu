using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Strategies.Favorites.FavoriteArticles
{
    public class FilterArticlesByListIdQueryStrategy : IFavoriteArticleQueryStrategy
    {
        public IQueryable<FavoriteArticle> Query(IQueryable<FavoriteArticle> favoriteArticles, FavoriteParameters favoriteParameters)
        {
            favoriteArticles = favoriteArticles
                .Where(fp => fp.FavoriteLists.Any(fl => fl.Id == favoriteParameters.FavoriteListId));
            return favoriteArticles;
        }
    }
}