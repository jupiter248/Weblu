using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IFavoritePortfolioQueryStrategy
    {
        IQueryable<FavoritePortfolio> Query(IQueryable<FavoritePortfolio> favoritePortfolios, FavoriteParameters favoriteParameters);
    }
}