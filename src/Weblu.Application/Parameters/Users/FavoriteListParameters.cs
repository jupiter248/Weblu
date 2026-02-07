using Weblu.Application.Common.Parameters;
using Weblu.Domain.Enums.Users.Favorites.Parameters;

namespace Weblu.Application.Parameters.Users
{
    public class FavoriteListParameters : BaseParameters
    {
        public FavoriteListTypeSort FavoriteListTypeSort { get; set; }
    }
}