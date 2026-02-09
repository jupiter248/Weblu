using Weblu.Application.DTOs.Portfolios.PortfolioDTOs.PortfolioImageDTOs;

namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioImageService
    {
        Task AddAsync(int portfolioId, int imageId, AddPortfolioImageDTO addPortfolioImageDTO);
        Task DeleteAsync(int portfolioId, int imageId);
    }
}