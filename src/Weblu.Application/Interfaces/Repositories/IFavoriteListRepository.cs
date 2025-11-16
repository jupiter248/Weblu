using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IFavoriteListRepository
    {
        Task<List<FavoriteList>> GetAllFavoriteListsAsync(string userId, FavoriteListParameters favoriteListParameters);
        Task<FavoriteList?> GetFavoriteListByIdAsync(int favoriteListId);
        Task AddFavoriteListAsync(FavoriteList favoriteList);
        void UpdateFavoriteList(FavoriteList favoriteList);
        void DeleteFavoriteList(FavoriteList favoriteList);

    }
}