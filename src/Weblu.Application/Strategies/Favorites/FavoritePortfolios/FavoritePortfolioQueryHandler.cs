using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Strategies.Favorites.FavoritePortfolios
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