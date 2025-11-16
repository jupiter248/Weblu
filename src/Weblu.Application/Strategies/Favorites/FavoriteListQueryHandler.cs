using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Strategies.Favorites
{
    public class FavoriteListQueryHandler
    {
        private readonly IFavoriteListQueryStrategy _favoriteListQueryStrategy;
        public FavoriteListQueryHandler(IFavoriteListQueryStrategy favoriteListQueryStrategy)
        {
            _favoriteListQueryStrategy = favoriteListQueryStrategy;
        }

        public IQueryable<FavoriteList> ExecuteFavoriteListQuery(IQueryable<FavoriteList> favoriteLists, FavoriteListParameters favoriteListParameters)
        {
            return _favoriteListQueryStrategy.Query(favoriteLists, favoriteListParameters);
        }
    }
}