using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IFavoriteArticleQueryStrategy
    {
        IQueryable<FavoriteArticle> Query(IQueryable<FavoriteArticle> favoritePortfolios, FavoriteParameters favoriteParameters);

    }
}