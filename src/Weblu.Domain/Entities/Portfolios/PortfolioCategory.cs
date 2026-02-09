using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Portfolios
{
    public class PortfolioCategory : BaseEntity
    {
        // Required properties
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        // Relationships
        public List<Portfolio> Portfolios { get; set; } = new();
    }
}