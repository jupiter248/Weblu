using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Interfaces.Repositories.Users.UserFavorites
{
    public interface IUserArticleFavoriteRepository
    {
        Task<IEnumerable<FavoriteArticle>> GetAllAsync(string userId, FavoriteParameters favoriteParameters);
        Task<FavoriteArticle?> GetByIdAsync(string userId, int favoriteArticleId);
        Task<FavoriteArticle?> GetByArticleIdAsync(string userId, int ArticleId);
        Task AddAsync(FavoriteArticle favoriteArticle);
        Task<bool> IsFavoriteAsync(string userId, int articleId);
        Task DeleteAsync(string userId, int articleId);
    }
}