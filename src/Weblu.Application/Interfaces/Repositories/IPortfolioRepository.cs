using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IPortfolioRepository : IGenericRepository<Portfolio, PortfolioParameters>
    {
        Task LoadContributorsAsync(Portfolio portfolio);
        Task LoadMethodsAsync(Portfolio portfolio);
        Task LoadFeaturesAsync(Portfolio portfolio);
        Task<Portfolio?> GetByIdWithImagesAsync(int portfolioId);
        Task<IEnumerable<Portfolio>> GetByTitleAsync(string title);

    }
}