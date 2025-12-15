using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Contributors;

namespace Weblu.Application.Strategies.Contributors
{
    public class FilterByPortfolioIdStrategy : IContributorQueryStrategy
    {
        public IQueryable<Contributor> Query(IQueryable<Contributor> contributors, ContributorParameters contributorParameters)
        {
            return contributors.Where(c => c.Portfolios.Any(p => p.Id == contributorParameters.FilterByPortfolioId));
        }
    }
}