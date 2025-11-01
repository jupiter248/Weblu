using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IPortfolioRepository
    {
        Task<List<Portfolio>> GetAllPortfolioAsync(PortfolioParameters portfolioParameters);
        Task<Portfolio?> GetPortfolioByIdAsync(int portfolioId);
        Task AddPortfolioAsync(Portfolio portfolio);
        void UpdatePortfolio(Portfolio portfolio);
        void DeletePortfolio(Portfolio portfolio);
    }
}