using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Contributors;

namespace Weblu.Application.Strategies.Common.Contributors
{
    public class FilterByArticleIdStrategy : IContributorQueryStrategy
    {
        public IQueryable<Contributor> Query(IQueryable<Contributor> contributors, ContributorParameters contributorParameters)
        {
            return contributors.Where(c => c.Articles.Any(a => a.Id == contributorParameters.FilterByPortfolioId));
        }
    }
}