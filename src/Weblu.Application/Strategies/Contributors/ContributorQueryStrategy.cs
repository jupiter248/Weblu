using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Strategies.Contributors
{
    public class ContributorQueryStrategy
    {
        private IContributorQueryStrategy _contributorQueryStrategy;
        public ContributorQueryStrategy(IContributorQueryStrategy contributorQueryStrategy)
        {
            _contributorQueryStrategy = contributorQueryStrategy;
        }
        public List<Contributor> ExecuteServiceQuery(List<Contributor> contributors, ContributorParameters contributorParameters)
        {
            return _contributorQueryStrategy.Query(contributors, contributorParameters);
        }
    }
}