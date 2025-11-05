using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Strategies.Portfolios
{
    public class FilteredByContributorIdStrategy : IPortfolioQueryStrategy
    {
        public List<Portfolio> Query(List<Portfolio> portfolios, PortfolioParameters portfolioParameters)
        {
            if (portfolioParameters.ContributorId.HasValue)
            {
                return portfolios.Where(p => p.Contributors.Any(c => c.Id == portfolioParameters.ContributorId)).ToList();
            }
            else
            {
                return portfolios;
            }
        }
    }
}