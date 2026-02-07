using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Interfaces.Strategies.Users
{
    public interface IFavoriteArticleQueryStrategy
    {
        IQueryable<FavoriteArticle> Query(IQueryable<FavoriteArticle> favoritePortfolios, FavoriteParameters favoriteParameters);

    }
}