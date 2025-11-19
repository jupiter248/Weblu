using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IPortfolioCategoryRepository
    {
        Task<IReadOnlyList<PortfolioCategory>> GetAllPortfolioCategoriesAsync();
        Task<PortfolioCategory?> GetPortfolioCategoryByIdAsync(int categoryId);
        Task AddPortfolioCategoryAsync(PortfolioCategory portfolioCategory);
        void UpdatePortfolioCategory(PortfolioCategory portfolioCategory);
        void DeletePortfolioCategory(PortfolioCategory portfolioCategory);
    }
}