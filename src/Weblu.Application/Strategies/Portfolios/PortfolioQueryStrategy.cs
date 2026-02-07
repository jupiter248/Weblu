using Weblu.Application.Interfaces.Strategies.Portfolios;
using Weblu.Application.Parameters.Portfolios;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Strategies.Portfolios
{
    public class PortfolioQueryStrategy
    {
        private IPortfolioQueryStrategy _portfolioQueryStrategy;
        public PortfolioQueryStrategy(IPortfolioQueryStrategy portfolioQueryStrategy)
        {
            _portfolioQueryStrategy = portfolioQueryStrategy;
        }
        public IQueryable<Portfolio> ExecutePortfolioQuery(IQueryable<Portfolio> portfolios, PortfolioParameters portfolioParameters)
        {
            return _portfolioQueryStrategy.Query(portfolios, portfolioParameters);
        }
    }
}