using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Portfolios.PortfolioDTOs;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Portfolios;

namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioService
    {
        Task<List<PortfolioSummaryDTO>> GetAllAsync(PortfolioParameters portfolioParameters);
        Task<PagedResponse<PortfolioSummaryDTO>> GetAllPagedAsync(PortfolioParameters portfolioParameters);
        Task<PortfolioDetailDTO> GetByIdAsync(int portfolioId);
        Task<PortfolioDetailDTO> CreateAsync(CreatePortfolioDTO createPortfolioDTO);
        Task<PortfolioDetailDTO> UpdateAsync(int portfolioId, UpdatePortfolioDTO updatePortfolioDTO);
        Task DeleteAsync(int portfolioId);
        Task Publish(int portfolioId);
        Task Unpublish(int portfolioId);
    }
}