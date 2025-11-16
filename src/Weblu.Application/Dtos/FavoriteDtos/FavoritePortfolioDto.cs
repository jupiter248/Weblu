using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.PortfolioDtos;

namespace Weblu.Application.Dtos.FavoriteDtos
{
    public class FavoritePortfolioDto
    {
        public int Id { get; set; }
        public PortfolioSummaryDto Portfolio { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string AddedAt { get; set; } = default!;
    }
}