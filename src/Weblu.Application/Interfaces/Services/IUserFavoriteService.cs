using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Interfaces.Services
{
    public interface IUserFavoriteService
    {
        Task<List<PortfolioSummaryDto>> GetAllFavoritePortfoliosAsync(string userId, FavoriteParameters favoriteParameters);
        Task AddPortfolioToFavorite(string userId, int portfolioId);
        Task DeletePortfolioFromFavorite(string userId, int portfolioId);
        Task<bool> IsFavorite(string userId, int portfolioId);
    }
}