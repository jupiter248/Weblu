using Weblu.Domain.Events.Common;

namespace Weblu.Domain.Events.Portfolios
{
    public class PortfolioPublishedEvent : IDomainEvent
    {
        public Guid PortfolioId { get; }
        public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
        public PortfolioPublishedEvent(Guid portfolioId)
        {
            PortfolioId = portfolioId;
        }
    }
}