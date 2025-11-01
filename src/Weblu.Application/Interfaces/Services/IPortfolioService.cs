using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services
{
    public interface IPortfolioService
    {
        Task<List<PortfolioSummaryDto>> GetAllPortfolioAsync(PortfolioParameters portfolioParameters);
        Task<PortfolioDetailDto> GetPortfolioByIdAsync(int portfolioId);
        Task<PortfolioDetailDto> AddPortfolioAsync(AddPortfolioDto addPortfolioDto);
        Task<PortfolioDetailDto> UpdatePortfolioAsync(int portfolioId, UpdatePortfolioDto updatePortfolioDto);
        Task DeletePortfolioAsync(int portfolioId);
        // Methods
        Task AddMethodToPortfolioAsync(int portfolioId, int methodId);
        Task DeleteMethodFromPortfolioAsync(int portfolioId, int methodId);
        // Feature
        Task AddFeatureToPortfolioAsync(int portfolioId, int featureId);
        Task DeleteFeatureFromPortfolioAsync(int portfolioId, int featureId);
        // Image 
    }
}