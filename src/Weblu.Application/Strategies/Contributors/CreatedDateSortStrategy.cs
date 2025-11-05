using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Contributors
{
    public class CreatedDateSortStrategy : IContributorQueryStrategy
    {
        public List<Contributor> Query(List<Contributor> contributors, ContributorParameters contributorParameters)
        {
            if (contributorParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return contributors.OrderByDescending(c => c.CreatedAt).ToList();
            }
            else if (contributorParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return contributors.OrderBy(c => c.CreatedAt).ToList();
            }
            else
            {
                return contributors;
            }
        }
    }
}