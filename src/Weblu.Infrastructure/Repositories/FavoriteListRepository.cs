using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.FavoriteLists;
using Weblu.Domain.Entities.Favorites;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class FavoriteListRepository : IFavoriteListRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteListRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddFavoriteListAsync(FavoriteList favoriteList)
        {
            await _context.FavoriteLists.AddAsync(favoriteList);
        }

        public void DeleteFavoriteList(FavoriteList favoriteList)
        {
            _context.FavoriteLists.Remove(favoriteList);
        }

        public async Task<FavoriteList?> GetFavoriteListByIdAsync(int favoriteListId)
        {
            FavoriteList? favoriteList = await _context.FavoriteLists.Include(f => f.FavoritePortfolios).FirstOrDefaultAsync(i => i.Id == favoriteListId);
            return favoriteList;
        }

        public async Task<List<FavoriteList>> GetAllFavoriteListsAsync(string userId, FavoriteListParameters favoriteListParameters)
        {
            IQueryable<FavoriteList> favoriteLists = _context.FavoriteLists.Where(u => u.UserId == userId).Include(f => f.FavoritePortfolios).AsQueryable();

            var favoriteListTypeSort = new FavoriteListQueryHandler(new FavoriteListTypeSortQueryStrategy());
            favoriteLists = favoriteListTypeSort.ExecuteFavoriteListQuery(favoriteLists, favoriteListParameters);

            return await favoriteLists.ToListAsync();
        }

        public void UpdateFavoriteList(FavoriteList favoriteList)
        {
            _context.FavoriteLists.Update(favoriteList);
        }
    }
}