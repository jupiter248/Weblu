using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Strategies.Favorites.FavoritePortfolios
{
    public class FilterPortfoliosByListIdQueryStrategy : IFavoritePortfolioQueryStrategy
    {
        public IQueryable<FavoritePortfolio> Query(IQueryable<FavoritePortfolio> favoritePortfolios, FavoriteParameters favoriteParameters)
        {
            favoritePortfolios = favoritePortfolios
                .Where(fp => fp.FavoriteLists.Any(fl => fl.Id == favoriteParameters.FavoriteListId));
            return favoritePortfolios;
        }
    }
}