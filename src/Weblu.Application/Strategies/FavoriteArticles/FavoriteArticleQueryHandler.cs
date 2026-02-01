using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Strategies.FavoriteArticles
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