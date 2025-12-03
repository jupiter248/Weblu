using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Portfolios;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddPortfolioAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
        }

        public void DeletePortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Remove(portfolio);
        }

        public async Task<IReadOnlyList<Portfolio>> GetAllPortfolioAsync(PortfolioParameters portfolioParameters)
        {
            IQueryable<Portfolio> portfolios = _context.Portfolios.Include(c => c.PortfolioCategory).Include(i => i.PortfolioImages).ThenInclude(i => i.ImageMedia).Include(c => c.Contributors);

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

            return await portfolios.ToListAsync();
        }

        public async Task<Portfolio?> GetPortfolioByIdAsync(int portfolioId)
        {
            Portfolio? portfolio = await _context.Portfolios.Include(c => c.PortfolioCategory).Include(c => c.Contributors).Include(f => f.Features).Include(m => m.Methods).Include(i => i.PortfolioImages).ThenInclude(i => i.ImageMedia).FirstOrDefaultAsync(p => p.Id == portfolioId);
            if (portfolio == null)
            {
                return null;
            }
            return portfolio;
        }

        public void UpdatePortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
        }
    }
}