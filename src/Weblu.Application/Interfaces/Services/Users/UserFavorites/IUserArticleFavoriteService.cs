using Weblu.Application.Dtos.ArticleDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services.Users.UserFavorites
{
    public interface IUserArticleFavoriteService
    {
        Task<List<ArticleSummaryDto>> GetAllAsync(string userId, FavoriteParameters favoriteParameters);
        Task AddAsync(string userId, int articleId);
        Task DeleteAsync(string userId, int articleId);
        Task<bool> IsFavoriteAsync(string userId, int articleId);
    }
}