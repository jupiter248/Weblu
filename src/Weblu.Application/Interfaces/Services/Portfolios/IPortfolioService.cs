using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Dtos.PortfolioDtos.PortfolioImageDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioService
    {
        Task<List<PortfolioSummaryDto>> GetAllPortfolioAsync(PortfolioParameters portfolioParameters);
        Task<PagedResponse<PortfolioSummaryDto>> GetAllPagedPortfolioAsync(PortfolioParameters portfolioParameters);
        Task<PortfolioDetailDto> GetPortfolioByIdAsync(int portfolioId);
        Task<PortfolioDetailDto> AddPortfolioAsync(AddPortfolioDto addPortfolioDto);
        Task<PortfolioDetailDto> UpdatePortfolioAsync(int portfolioId, UpdatePortfolioDto updatePortfolioDto);
        Task DeletePortfolioAsync(int portfolioId);
    }
}