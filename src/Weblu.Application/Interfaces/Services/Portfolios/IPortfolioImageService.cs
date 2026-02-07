using Weblu.Application.Dtos.Portfolios.PortfolioDtos.PortfolioImageDtos;

namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioImageService
    {
        Task AddImageAsync(int portfolioId, int imageId, AddPortfolioImageDto addPortfolioImageDto);
        Task DeleteImageAsync(int portfolioId, int imageId);
    }
}