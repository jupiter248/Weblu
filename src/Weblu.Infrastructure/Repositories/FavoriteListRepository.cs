using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.FavoriteLists;
using Weblu.Domain.Entities.Favorites;
using Weblu.Domain.Enums.Favorites.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;

namespace Weblu.Infrastructure.Repositories
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