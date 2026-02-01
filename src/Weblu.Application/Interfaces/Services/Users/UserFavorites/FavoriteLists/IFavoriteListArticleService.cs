using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Interfaces.Services.Users.UserFavorites.FavoriteLists
{
    public interface IFavoriteListArticleService
    {
        Task AddAsync(string userId, int favoriteListId, int articleId);
        Task DeleteAsync(string userId, int favoriteListId, int articleId);
    }
}