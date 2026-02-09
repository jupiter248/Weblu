using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Portfolios.PortfolioDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Portfolios;

namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioService
    {
        Task<List<PortfolioSummaryDto>> GetAllAsync(PortfolioParameters portfolioParameters);
        Task<PagedResponse<PortfolioSummaryDto>> GetAllPagedAsync(PortfolioParameters portfolioParameters);
        Task<PortfolioDetailDto> GetByIdAsync(int portfolioId);
        Task<PortfolioDetailDto> CreateAsync(CreatePortfolioDto createPortfolioDto);
        Task<PortfolioDetailDto> UpdateAsync(int portfolioId, UpdatePortfolioDto updatePortfolioDto);
        Task DeleteAsync(int portfolioId);
        Task Publish(int portfolioId);
        Task Unpublish(int portfolioId);
    }
}