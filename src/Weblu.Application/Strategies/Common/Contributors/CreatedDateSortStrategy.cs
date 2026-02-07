using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Contributors;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Common.Contributors
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