using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IPortfolioQueryStrategy
    {
        IQueryable<Portfolio> Query(IQueryable<Portfolio> portfolios, PortfolioParameters portfolioParameters);
    }
}