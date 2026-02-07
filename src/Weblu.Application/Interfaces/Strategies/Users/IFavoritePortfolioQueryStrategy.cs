using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Interfaces.Strategies.Users
{
    public interface IFavoritePortfolioQueryStrategy
    {
        IQueryable<FavoritePortfolio> Query(IQueryable<FavoritePortfolio> favoritePortfolios, FavoriteParameters favoriteParameters);
    }
}