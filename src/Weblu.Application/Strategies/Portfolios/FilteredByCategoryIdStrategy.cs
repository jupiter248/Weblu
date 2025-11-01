using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Strategies.Portfolios
{
    public class FilteredByCategoryIdStrategy : IPortfolioQueryStrategy
    {
        public List<Portfolio> Query(List<Portfolio> portfolios, PortfolioParameters portfolioParameters)
        {
            if (portfolioParameters.CategoryId.HasValue)
            {
                return portfolios.Where(p => p.PortfolioCategoryId == portfolioParameters.CategoryId).ToList();
            }
            else
            {
                return portfolios;
            }
        }
    }
}