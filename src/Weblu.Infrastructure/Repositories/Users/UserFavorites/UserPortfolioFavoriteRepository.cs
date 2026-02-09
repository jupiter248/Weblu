using Microsoft.EntityFrameworkCore;
using Weblu.Infrastructure.Data;
using Weblu.Application.Interfaces.Repositories.Users.UserFavorites;
using Weblu.Domain.Entities.Users.Favorites;
using Weblu.Application.Strategies.Favorites.FavoritePortfolios;
using Weblu.Application.Parameters.Users;

namespace Weblu.Infrastructure.Repositories.Users.UserFavorites
{
    public class UserPortfolioFavoriteRepository : IUserPortfolioFavoriteRepository
    {
        private readonly ApplicationDbContext _context;
        public UserPortfolioFavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FavoritePortfolio favoritePortfolio)
        {
            await _context.FavoritePortfolios.AddAsync(favoritePortfolio);
        }

        public async Task<bool> IsFavoriteAsync(string userId, int portfolioId)
        {
            return await _context.FavoritePortfolios.AnyAsync(f => f.UserId == userId && f.PortfolioId == portfolioId);
        }

        public async Task DeleteAsync(string userId, int portfolioId)
        {
            FavoritePortfolio? favoritePortfolio = await _context.FavoritePortfolios.FirstOrDefaultAsync(f => f.UserId == userId && f.PortfolioId == portfolioId);
            if (favoritePortfolio != null) _context.FavoritePortfolios.Remove(favoritePortfolio);
        }

        public async Task<IReadOnlyList<FavoritePortfolio>> GetAllAsync(string userId, FavoriteParameters favoriteParameters)
        {
            IQueryable<FavoritePortfolio> favoritePortfolios = _context.FavoritePortfolios.Where(u => u.UserId == userId).Include(p => p.Portfolio).Include(f => f.FavoriteLists).AsNoTracking();
            if (favoriteParameters.FavoriteListId.HasValue)
            {
                favoritePortfolios = new FavoritePortfolioQueryHandler(new FilterPortfoliosByListIdQueryStrategy())
                .ExecuteFavoritePortfolioQuery(favoritePortfolios, favoriteParameters);
            }

            return await favoritePortfolios.ToListAsync();
        }

        public async Task<FavoritePortfolio?> GetByIdAsync(string userId, int favoritePortfolioId)
        {
            FavoritePortfolio? favoritePortfolio = await _context.FavoritePortfolios.Where(u => u.UserId == userId).Include(p => p.Portfolio).FirstOrDefaultAsync(f => f.Id == favoritePortfolioId);
            return favoritePortfolio;
        }

        public async Task<FavoritePortfolio?> GetByPortfolioIdAsync(string userId, int portfolioId)
        {
            FavoritePortfolio? favoritePortfolio = await _context.FavoritePortfolios.FirstOrDefaultAsync(u => u.UserId == userId && u.PortfolioId == portfolioId);
            return favoritePortfolio;
        }
    }
}