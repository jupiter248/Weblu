using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Contributors;
using Weblu.Domain.Entities.Common;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class ContributorRepository : IContributorRepository
    {
        private readonly ApplicationDbContext _context;
        public ContributorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddContributorAsync(Contributor contributor)
        {
            await _context.Contributors.AddAsync(contributor);
        }

        public async Task<bool> ContributorExistsAsync(int contributorId)
        {
            bool ContributorExists = await _context.Contributors.AnyAsync(c => c.Id == contributorId);

            return ContributorExists;
        }

        public void DeleteContributor(Contributor contributor)
        {
            _context.Contributors.Remove(contributor);
        }

        public async Task<IReadOnlyList<Contributor>> GetAllContributorsAsync(ContributorParameters contributorParameters)
        {
            IQueryable<Contributor> contributors = _context.Contributors.AsQueryable();

            var createdDateSortStrategy = new ContributorQueryStrategy(new CreatedDateSortStrategy());
            contributors = createdDateSortStrategy.ExecuteServiceQuery(contributors, contributorParameters);

            return await contributors.ToListAsync();
        }

        public async Task<Contributor?> GetContributorByIdAsync(int contributorId)
        {
            Contributor? contributor = await _context.Contributors.FirstOrDefaultAsync(c => c.Id == contributorId);
            if (contributor == null)
            {
                return null;
            }
            return contributor;
        }

        public void UpdateContributor(Contributor contributor)
        {
            _context.Contributors.Update(contributor);
        }
    }
}