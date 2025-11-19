using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Dtos.FavoriteDtos;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.FavoritePortfolios;
using Weblu.Domain.Entities.Favorites;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class UserFavoritesRepository : IUserFavoritesRepository
    {
        private readonly ApplicationDbContext _context;
        public UserFavoritesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<FavoritePortfolio>> GetAllFavoritePortfoliosAsync(string userId, FavoriteParameters favoriteParameters)
        {
            IQueryable<FavoritePortfolio> favoritePortfolios = _context.FavoritePortfolios.Where(u => u.UserId == userId).Include(p => p.Portfolio).Include(f => f.FavoriteLists).AsQueryable();

            var filteredByListIdQuery = new FavoritePortfolioQueryHandler(new FilterByListIdQueryStrategy());
            favoritePortfolios = filteredByListIdQuery.ExecuteFavoritePortfolioQuery(favoritePortfolios, favoriteParameters);

            return await favoritePortfolios.ToListAsync();
        }

        public async Task<FavoritePortfolio?> GetFavoritePortfolioByIdAsync(string userId, int favoritePortfolioId)
        {
            FavoritePortfolio? favoritePortfolio = await _context.FavoritePortfolios.Where(u => u.UserId == userId).Include(p => p.Portfolio).FirstOrDefaultAsync(f => f.Id == favoritePortfolioId);
            return favoritePortfolio;
        }

        public async Task<FavoritePortfolio?> GetFavoritePortfolioByPortfolioIdAsync(string userId, int portfolioId)
        {
            FavoritePortfolio? favoritePortfolio = await _context.FavoritePortfolios.FirstOrDefaultAsync(u => u.UserId == userId && u.PortfolioId == portfolioId);
            return favoritePortfolio;
        }
    }
}