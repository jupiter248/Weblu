using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Favorites
{
    public class FavoriteListErrorCodes
    {
        public const string NotFound = "FAVORITE_LIST_NOT_FOUND";
        public const string DeleteForbidden = "FAVORITE_LIST_DELETE_FORBIDDEN";
        public const string UpdateForbidden = "FAVORITE_LIST_UPDATE_FORBIDDEN";

    }
}