using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IUserFavoritesRepository
    {
        Task<IReadOnlyList<FavoritePortfolio>> GetAllFavoritePortfoliosAsync(string userId, FavoriteParameters favoriteParameters);
        Task<FavoritePortfolio?> GetFavoritePortfolioByIdAsync(string userId, int favoritePortfolioId);
        Task<FavoritePortfolio?> GetFavoritePortfolioByPortfolioIdAsync(string userId, int portfolioId);
    }
}