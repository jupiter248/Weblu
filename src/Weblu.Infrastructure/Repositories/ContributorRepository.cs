using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Contributors;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    internal class ContributorRepository : GenericRepository<Contributor, ContributorParameters>, IContributorRepository
    {
        public ContributorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IReadOnlyList<Contributor>> GetAllAsync(ContributorParameters contributorParameters)
        {
            IQueryable<Contributor> contributors = _context.Contributors;

            if (contributorParameters.CreatedDateSort != CreatedDateSort.All)
            {
                contributors = new ContributorQueryHandler(new CreatedDateSortStrategy())
                .ExecuteContributorQuery(contributors, contributorParameters);
            }

            return await contributors.ToListAsync();
        }
    }
}