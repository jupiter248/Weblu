using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Media;

namespace Weblu.Domain.Entities.Portfolios
{
    public class PortfolioImage : BaseEntity
    {
        // Required properties
        public bool IsThumbnail { get; set; }
        // Relationships
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; } = default!;
        public int ImageMediaId { get; set; }
        public ImageMedia ImageMedia { get; set; } = default!;
    }
}