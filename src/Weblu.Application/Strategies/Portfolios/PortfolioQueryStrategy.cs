using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
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
        public IQueryable<Portfolio> ExecuteServiceQuery(IQueryable<Portfolio> portfolios, PortfolioParameters portfolioParameters)
        {
            return _portfolioQueryStrategy.Query(portfolios, portfolioParameters);
        }
    }
}