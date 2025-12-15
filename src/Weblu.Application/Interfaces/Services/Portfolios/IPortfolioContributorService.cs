using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioContributorService
    {
        Task AddContributorAsync(int portfolioId, int contributorId);
        Task DeleteContributorAsync(int portfolioId, int contributorId);
    }
}