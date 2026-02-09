using Microsoft.EntityFrameworkCore;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;
using Weblu.Domain.Entities.Common.Contributors;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Strategies.Common.Contributors;

namespace Weblu.Infrastructure.Repositories.Common
{
    internal class ContributorRepository : GenericRepository<Contributor, ContributorParameters>, IContributorRepository
    {
        public ContributorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<PagedList<Contributor>> GetAllAsync(ContributorParameters contributorParameters)
        {
            IQueryable<Contributor> contributors = _context.Contributors.Where(a => !a.IsDeleted).AsNoTracking();

            if (contributorParameters.CreatedDateSort != CreatedDateSort.All)
            {
                contributors = new ContributorQueryHandler(new CreatedDateSortStrategy())
                .ExecuteContributorQuery(contributors, contributorParameters);
            }
            if (contributorParameters.FilterByPortfolioId.HasValue)
            {
                contributors = new ContributorQueryHandler(new FilterByPortfolioIdStrategy())
                .ExecuteContributorQuery(contributors.Include(p => p.Portfolios), contributorParameters);
            }
            if (contributorParameters.FilterByArticleId.HasValue)
            {
                contributors = new ContributorQueryHandler(new FilterByPortfolioIdStrategy())
                .ExecuteContributorQuery(contributors.Include(p => p.Articles), contributorParameters);
            }

            return await PaginationExtensions<Contributor>.GetPagedList(contributors, contributorParameters.PageNumber, contributorParameters.PageSize);
        }
    }
}