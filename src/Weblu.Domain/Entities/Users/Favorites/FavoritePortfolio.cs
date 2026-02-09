using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Domain.Entities.Users.Favorites
{
    public class FavoritePortfolio : BaseEntity
    {
        // Relationships
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public List<FavoriteList> FavoriteLists { get; set; } = new();
    }
}