using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
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