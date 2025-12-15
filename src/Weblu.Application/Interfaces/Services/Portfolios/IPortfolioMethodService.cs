using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioMethodService
    {
        Task AddMethodAsync(int portfolioId, int methodId);
        Task DeleteMethodAsync(int portfolioId, int methodId);
    }
}