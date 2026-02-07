using Weblu.Application.Interfaces.Strategies.Portfolios;
using Weblu.Application.Parameters.Portfolios;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Strategies.Portfolios
{
    public class FilterByCategoryIdStrategy : IPortfolioQueryStrategy
    {
        public IQueryable<Portfolio> Query(IQueryable<Portfolio> portfolios, PortfolioParameters portfolioParameters)
        {
            return portfolios.Where(p => p.PortfolioCategoryId == portfolioParameters.CategoryId);
        }
    }
}