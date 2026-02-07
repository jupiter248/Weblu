using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Contributors;

namespace Weblu.Application.Strategies.Common.Contributors
{
    public class ContributorQueryHandler
    {
        private IContributorQueryStrategy _contributorQueryStrategy;
        public ContributorQueryHandler(IContributorQueryStrategy contributorQueryStrategy)
        {
            _contributorQueryStrategy = contributorQueryStrategy;
        }
        public IQueryable<Contributor> ExecuteContributorQuery(IQueryable<Contributor> contributors, ContributorParameters contributorParameters)
        {
            return _contributorQueryStrategy.Query(contributors, contributorParameters);
        }
    }
}