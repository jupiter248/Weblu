using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Contributors;

namespace Weblu.Application.Interfaces.Strategies.Common
{
    public interface IContributorQueryStrategy
    {
        IQueryable<Contributor> Query(IQueryable<Contributor> contributors, ContributorParameters contributorParameters);
    }
}