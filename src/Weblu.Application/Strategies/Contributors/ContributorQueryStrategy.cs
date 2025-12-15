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