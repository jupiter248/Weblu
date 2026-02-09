using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Contributors;

namespace Weblu.Application.Interfaces.Repositories.Common
{
    public interface IContributorRepository : IGenericRepository<Contributor, ContributorParameters>
    {
    }
}