using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Strategies.Favorites.FavoriteArticles
{
    public class FavoriteArticleQueryHandler
    {
        private readonly IFavoriteArticleQueryStrategy _favoriteArticleQueryStrategy;
        public FavoriteArticleQueryHandler(IFavoriteArticleQueryStrategy favoriteArticleQueryStrategy)
        {
            _favoriteArticleQueryStrategy = favoriteArticleQueryStrategy;
        }

        public IQueryable<FavoriteArticle> ExecuteFavoriteArticlesQuery(IQueryable<FavoriteArticle> favoriteArticles, FavoriteParameters favoriteListParameters)
        {
            return _favoriteArticleQueryStrategy.Query(favoriteArticles, favoriteListParameters);
        }
    }
}