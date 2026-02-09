using Microsoft.EntityFrameworkCore;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Domain.Entities.Users.Favorites;
using Weblu.Application.Parameters.Users;
using Weblu.Application.Strategies.Favorites.FavoriteLists;
using Weblu.Domain.Enums.Users.Favorites.Parameters;
using Weblu.Application.Interfaces.Repositories.Users.Favorites;

namespace Weblu.Infrastructure.Repositories.Users.UserFavorites
{
    internal class FavoriteListRepository : GenericRepository<FavoriteList, FavoriteListParameters>, IFavoriteListRepository
    {
        public FavoriteListRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<FavoriteList?> GetByIdAsync(int favoriteListId)
        {
            FavoriteList? favoriteList = await _context.FavoriteLists.Include(f => f.FavoritePortfolios).FirstOrDefaultAsync(i => i.Id == favoriteListId);
            return favoriteList;
        }

        public async Task<IReadOnlyList<FavoriteList>> GetAllByUserIdAsync(string userId, FavoriteListParameters favoriteListParameters)
        {
            IQueryable<FavoriteList> favoriteLists = _context.FavoriteLists.Where(u => u.UserId == userId).Include(f => f.FavoritePortfolios).Include(a => a.FavoriteArticles).AsNoTracking().AsQueryable();

            if (favoriteListParameters.FavoriteListTypeSort != FavoriteListTypeSort.All)
            {
                favoriteLists = new FavoriteListQueryHandler(new FavoriteListTypeSortQueryStrategy())
                .ExecuteFavoriteListQuery(favoriteLists, favoriteListParameters);
            }
            return await favoriteLists.ToListAsync();
        }

        public async Task<FavoriteList?> GetByUserAndListIdAsync(string userId, int favoriteListId)
        {
            FavoriteList? favoriteList = await _context.FavoriteLists.Include(f => f.FavoritePortfolios).Include(a => a.FavoriteArticles).FirstOrDefaultAsync(i => i.Id == favoriteListId && i.UserId == userId);
            return favoriteList;
        }
    }
}