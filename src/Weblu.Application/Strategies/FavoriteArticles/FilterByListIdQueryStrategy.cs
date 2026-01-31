using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Strategies.FavoriteArticles
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