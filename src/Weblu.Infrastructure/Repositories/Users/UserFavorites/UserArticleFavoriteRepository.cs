using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories.Users.UserFavorites;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.FavoriteArticles;
using Weblu.Domain.Entities.Favorites;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories.Users.UserFavorites
{
    public class UserArticleFavoriteRepository : IUserArticleFavoriteRepository
    {
        private readonly ApplicationDbContext _context;
        public UserArticleFavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FavoriteArticle favoriteArticle)
        {
            await _context.FavoriteArticles.AddAsync(favoriteArticle);
        }

        public async Task<bool> IsFavoriteAsync(string userId, int articleId)
        {
            return await _context.FavoriteArticles.AnyAsync(f => f.UserId == userId && f.ArticleId == articleId);
        }

        public async Task DeleteAsync(string userId, int articleId)
        {
            FavoriteArticle? favoriteArticle = await _context.FavoriteArticles.FirstOrDefaultAsync(f => f.UserId == userId && f.ArticleId == articleId);
            if (favoriteArticle != null) _context.FavoriteArticles.Remove(favoriteArticle);
        }

        public async Task<IEnumerable<FavoriteArticle>> GetAllAsync(string userId, FavoriteParameters favoriteParameters)
        {
            IQueryable<FavoriteArticle> favoriteArticles = _context.FavoriteArticles.Where(u => u.UserId == userId).Include(p => p.Article).Include(f => f.FavoriteLists).AsNoTracking();
            if (favoriteParameters.FavoriteListId.HasValue)
            {
                favoriteArticles = new FavoriteArticleQueryHandler(new FilterArticlesByListIdQueryStrategy())
                .ExecuteFavoriteArticlesQuery(favoriteArticles, favoriteParameters);
            }

            return await favoriteArticles.ToListAsync();
        }

        public async Task<FavoriteArticle?> GetByIdAsync(string userId, int favoritePortfolioId)
        {
            FavoriteArticle? favoriteArticles = await _context.FavoriteArticles.Where(u => u.UserId == userId).Include(p => p.Article).FirstOrDefaultAsync(f => f.Id == favoritePortfolioId);
            return favoriteArticles;
        }

        public async Task<FavoriteArticle?> GetByArticleIdAsync(string userId, int portfolioId)
        {
            FavoriteArticle? favoriteArticles = await _context.FavoriteArticles.FirstOrDefaultAsync(u => u.UserId == userId && u.ArticleId == portfolioId);
            return favoriteArticles;
        }
    }
}