using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;
using Weblu.Domain.Enums.Favorites;
using Weblu.Domain.Enums.Favorites.Parameters;

namespace Weblu.Application.Strategies.Favorites
{
    public class FavoriteListTypeSortQueryStrategy : IFavoriteListQueryStrategy
    {
        public IQueryable<FavoriteList> Query(IQueryable<FavoriteList> favoriteLists, FavoriteListParameters favoriteListParameters)
        {
            if (favoriteListParameters.FavoriteListTypeSort == FavoriteListTypeSort.Portfolio)
            {
                return favoriteLists.Where(t => t.FavoriteListType == FavoriteListType.Portfolio);
            }
            else if (favoriteListParameters.FavoriteListTypeSort == FavoriteListTypeSort.Article)
            {
                return favoriteLists.Where(t => t.FavoriteListType == FavoriteListType.Article);
            }
            else
            {
                return favoriteLists;
            }
        }
    }
}