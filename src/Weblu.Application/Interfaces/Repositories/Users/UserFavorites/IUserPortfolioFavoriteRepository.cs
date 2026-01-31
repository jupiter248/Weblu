using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Interfaces.Repositories.Users.UserFavorites
{
    public interface IUserPortfolioFavoriteRepository
    {
        Task<IReadOnlyList<FavoritePortfolio>> GetAllAsync(string userId, FavoriteParameters favoriteParameters);
        Task<FavoritePortfolio?> GetByIdAsync(string userId, int favoritePortfolioId);
        Task<FavoritePortfolio?> GetByPortfolioIdAsync(string userId, int portfolioId);
        Task AddAsync(FavoritePortfolio favoritePortfolio);
        Task<bool> IsFavoriteAsync(string userId, int portfolioId);
        Task DeleteAsync(string userId, int portfolioId);
    }
}