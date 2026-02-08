using Weblu.Application.Dtos.Portfolios.PortfolioDtos.PortfolioImageDtos;

namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioImageService
    {
        Task AddAsync(int portfolioId, int imageId, AddPortfolioImageDto addPortfolioImageDto);
        Task DeleteAsync(int portfolioId, int imageId);
    }
}