using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IFavoriteListRepository :  IGenericRepository<FavoriteList, FavoriteListParameters>
    {
        Task<IReadOnlyList<FavoriteList>> GetAllByUserIdAsync(string userId, FavoriteListParameters favoriteListParameters);
    }
}