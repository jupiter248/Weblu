using Weblu.Application.Parameters.Portfolios;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Interfaces.Strategies.Portfolios
{
    public interface IPortfolioQueryStrategy
    {
        IQueryable<Portfolio> Query(IQueryable<Portfolio> portfolios, PortfolioParameters portfolioParameters);
    }
}