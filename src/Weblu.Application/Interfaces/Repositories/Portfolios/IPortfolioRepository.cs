using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Portfolios;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Interfaces.Repositories.Portfolios
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