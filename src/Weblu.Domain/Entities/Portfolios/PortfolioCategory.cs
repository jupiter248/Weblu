using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Portfolios
{
    public class PortfolioCategory : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}