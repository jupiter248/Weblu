using Weblu.Application.Interfaces.Strategies.Portfolios;
using Weblu.Application.Parameters.Portfolios;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Portfolios
{
    public class CreatedDateSortStrategy : IPortfolioQueryStrategy
    {
        public IQueryable<Portfolio> Query(IQueryable<Portfolio> portfolios, PortfolioParameters portfolioParameters)
        {
            if (portfolioParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return portfolios.OrderByDescending(s => s.CreatedAt);
            }
            else if (portfolioParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return portfolios.OrderBy(s => s.CreatedAt);
            }
            else
            {
                return portfolios;
            }
        }
    }
}