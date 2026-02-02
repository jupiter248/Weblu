using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Portfolios;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;

namespace Weblu.Infrastructure.Repositories
{
    internal class PortfolioRepository : GenericRepository<Portfolio, PortfolioParameters>, IPortfolioRepository
    {
        public PortfolioRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<PagedList<Portfolio>> GetAllAsync(PortfolioParameters portfolioParameters)
        {
            IQueryable<Portfolio> portfolios = _context.Portfolios.Include(c => c.PortfolioCategory).Include(i => i.PortfolioImages.Where(im => im.IsThumbnail)).ThenInclude(i => i.ImageMedia).AsNoTracking();

            if (portfolioParameters.CreatedDateSort != CreatedDateSort.All)
            {
                portfolios = new PortfolioQueryStrategy(new CreatedDateSortStrategy())
                .ExecutePortfolioQuery(portfolios, portfolioParameters);
            }
            if (portfolioParameters.CategoryId.HasValue)
            {
                portfolios = new PortfolioQueryStrategy(new FilterByCategoryIdStrategy())
                .ExecutePortfolioQuery(portfolios, portfolioParameters);
            }
            if (portfolioParameters.ContributorId.HasValue)
            {
                portfolios = new PortfolioQueryStrategy(new FilterByContributorIdStrategy())
                .ExecutePortfolioQuery(portfolios, portfolioParameters);
            }
            return await PaginationExtensions<Portfolio>.GetPagedList(portfolios, portfolioParameters.PageNumber, portfolioParameters.PageSize);
        }

        public override async Task<Portfolio?> GetByIdAsync(int portfolioId)
        {
            Portfolio? portfolio = await _context.Portfolios.Include(c => c.PortfolioCategory).FirstOrDefaultAsync(p => p.Id == portfolioId);
            return portfolio;
        }

        public async Task<Portfolio?> GetByIdWithImagesAsync(int portfolioId)
        {
            Portfolio? portfolio = await _context.Portfolios.Include(c => c.PortfolioCategory).Include(i => i.PortfolioImages).ThenInclude(i => i.ImageMedia).FirstOrDefaultAsync(p => p.Id == portfolioId);
            return portfolio;
        }

        public async Task<IEnumerable<Portfolio>> GetByTitleAsync(string title)
        {
            return await _context.Portfolios.Where(a => a.Title.ToLower().Contains(title.ToLower())).ToListAsync();
        }

        public async Task LoadContributorsAsync(Portfolio portfolio)
        {
            await _context.Entry(portfolio).Collection(p => p.Contributors).LoadAsync();
        }

        public async Task LoadFeaturesAsync(Portfolio portfolio)
        {
            await _context.Entry(portfolio).Collection(p => p.Features).LoadAsync();
        }

        public async Task LoadMethodsAsync(Portfolio portfolio)
        {
            await _context.Entry(portfolio).Collection(p => p.Methods).LoadAsync();
        }
    }
}