using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Strategies.FavoritePortfolios
{
    public class FilterByListIdQueryStrategy : IFavoritePortfolioQueryStrategy
    {
        public IQueryable<FavoritePortfolio> Query(IQueryable<FavoritePortfolio> favoritePortfolios, FavoriteParameters favoriteParameters)
        {
            if (favoriteParameters.FavoriteListId.HasValue)
            {
                int targetListId = favoriteParameters.FavoriteListId.Value;

                favoritePortfolios = favoritePortfolios
                    .Where(fp => fp.FavoriteLists.Any(fl => fl.Id == targetListId));
            }
            return favoritePortfolios;
        }
    }
}