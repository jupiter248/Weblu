using Weblu.Application.Dtos.Portfolios.PortfolioDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Users;

namespace Weblu.Application.Interfaces.Services.Users.Favorites
{
    public interface IUserPortfolioFavoriteService
    {
        Task<List<PortfolioSummaryDto>> GetAllAsync(string userId, FavoriteParameters favoriteParameters);
        Task AddAsync(string userId, int portfolioId);
        Task DeleteAsync(string userId, int portfolioId);
        Task<bool> IsFavoriteAsync(string userId, int portfolioId);
    }
}