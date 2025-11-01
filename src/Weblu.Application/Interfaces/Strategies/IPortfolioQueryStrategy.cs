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
        List<Portfolio> Query(List<Portfolio> portfolios, PortfolioParameters portfolioParameters);
    }
}