using Weblu.Application.Dtos.Articles.ArticleDtos;
using Weblu.Application.Parameters.Users;

namespace Weblu.Application.Interfaces.Services.Users.Favorites
{
    public interface IUserArticleFavoriteService
    {
        Task<List<ArticleSummaryDto>> GetAllAsync(string userId, FavoriteParameters favoriteParameters);
        Task AddAsync(string userId, int articleId);
        Task DeleteAsync(string userId, int articleId);
        Task<bool> IsFavoriteAsync(string userId, int articleId);
    }
}