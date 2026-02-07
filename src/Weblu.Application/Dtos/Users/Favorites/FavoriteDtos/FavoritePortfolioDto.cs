using Weblu.Application.Dtos.Portfolios.PortfolioDtos;

namespace Weblu.Application.Dtos.Users.Favorites.FavoriteDtos
{
    public class FavoritePortfolioDto
    {
        public int Id { get; set; }
        public PortfolioSummaryDto Portfolio { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string AddedAt { get; set; } = default!;
    }
}