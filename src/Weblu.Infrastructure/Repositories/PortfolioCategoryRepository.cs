using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class PortfolioCategoryRepository : IPortfolioCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddPortfolioCategoryAsync(PortfolioCategory portfolioCategory)
        {
            await _context.PortfolioCategories.AddAsync(portfolioCategory);
        }

        public void DeletePortfolioCategory(PortfolioCategory portfolioCategory)
        {
            _context.PortfolioCategories.Remove(portfolioCategory);
        }

        public async Task<List<PortfolioCategory>> GetAllPortfolioCategoriesAsync()
        {
            List<PortfolioCategory> portfolioCategories = await _context.PortfolioCategories.ToListAsync();
            return portfolioCategories;
        }

        public async Task<PortfolioCategory?> GetPortfolioCategoryByIdAsync(int categoryId)
        {
            PortfolioCategory? portfolioCategory = await _context.PortfolioCategories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (portfolioCategory == null)
            {
                return null;
            }
            return portfolioCategory;
        }

        public void UpdatePortfolioCategory(PortfolioCategory portfolioCategory)
        {
            _context.PortfolioCategories.Update(portfolioCategory);
        }
    }
}