using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IContributorRepository
    {
        Task<IReadOnlyList<Contributor>> GetAllContributorsAsync(ContributorParameters contributorParameters);
        Task<Contributor?> GetContributorByIdAsync(int contributorId);
        Task<bool> ContributorExistsAsync(int contributorId);
        Task AddContributorAsync(Contributor contributor);
        void UpdateContributor(Contributor contributor);
        void DeleteContributor(Contributor contributor);
    }
}