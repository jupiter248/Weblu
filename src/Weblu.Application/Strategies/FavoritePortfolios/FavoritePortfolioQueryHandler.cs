using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Strategies.FavoritePortfolios
{
    public class FavoritePortfolioQueryHandler
    {
        private readonly IFavoritePortfolioQueryStrategy _favoritePortfolioQueryStrategy;
        public FavoritePortfolioQueryHandler(IFavoritePortfolioQueryStrategy favoritePortfolioQueryStrategy)
        {
            _favoritePortfolioQueryStrategy = favoritePortfolioQueryStrategy;
        }

        public IQueryable<FavoritePortfolio> ExecuteFavoritePortfolioQuery(IQueryable<FavoritePortfolio> favoritePortfolios, FavoriteParameters favoriteListParameters)
        {
            return _favoritePortfolioQueryStrategy.Query(favoritePortfolios, favoriteListParameters);
        }
    }
}