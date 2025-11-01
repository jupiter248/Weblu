using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.PortfolioDtos
{
    public class PortfolioSummaryDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
        public string? ThumbnailPictureUrl { get; set; }
    }
}