using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Portfolios;
using Weblu.Domain.Entities.Portfolios;
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

        public async Task<List<Portfolio>> GetAllPortfolioAsync(PortfolioParameters portfolioParameters)
        {
            List<Portfolio> portfolios = await _context.Portfolios.Include(c => c.PortfolioCategory).Include(i => i.PortfolioImages).ThenInclude(i => i.ImageMedia).ToListAsync();

            var createdDateSort = new PortfolioQueryStrategy(new CreatedDateSortStrategy());
            portfolios = createdDateSort.ExecuteServiceQuery(portfolios, portfolioParameters);

            var filteredByCategoryId = new PortfolioQueryStrategy(new FilteredByCategoryIdStrategy());
            portfolios = filteredByCategoryId.ExecuteServiceQuery(portfolios, portfolioParameters);

            return portfolios;
        }

        public async Task<Portfolio?> GetPortfolioByIdAsync(int portfolioId)
        {
            Portfolio? portfolio = await _context.Portfolios.Include(c => c.PortfolioCategory).Include(f => f.Features).Include(m => m.Methods).Include(i => i.PortfolioImages).ThenInclude(i => i.ImageMedia).FirstOrDefaultAsync(p => p.Id == portfolioId);
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