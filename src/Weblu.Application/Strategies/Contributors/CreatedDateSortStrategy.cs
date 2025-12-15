using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Contributors;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Contributors
{
    public class CreatedDateSortStrategy : IContributorQueryStrategy
    {
        public IQueryable<Contributor> Query(IQueryable<Contributor> contributors, ContributorParameters contributorParameters)
        {
            if (contributorParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return contributors.OrderByDescending(c => c.CreatedAt);
            }
            else if (contributorParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return contributors.OrderBy(c => c.CreatedAt);
            }
            else
            {
                return contributors;
            }
        }
    }
}