using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Interfaces.Services.Users.UserFavorites.FavoriteLists
{
    public interface IFavoriteListPortfolioService
    {
        Task AddAsync(string userId, int favoriteListId, int portfolioId);
        Task DeleteAsync(string userId, int favoriteListId, int portfolioId);
    }
}