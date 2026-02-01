using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services.Users.UserFavorites
{
    public interface IUserPortfolioFavoriteService
    {
        Task<List<PortfolioSummaryDto>> GetAllAsync(string userId, FavoriteParameters favoriteParameters);
        Task AddAsync(string userId, int portfolioId);
        Task DeleteAsync(string userId, int portfolioId);
        Task<bool> IsFavoriteAsync(string userId, int portfolioId);
    }
}