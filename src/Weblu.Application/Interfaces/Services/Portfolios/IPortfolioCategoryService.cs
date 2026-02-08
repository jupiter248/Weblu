using Weblu.Application.Dtos.Portfolios.PortfolioCategory;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Portfolios;

namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioCategoryService
    {
        Task<List<PortfolioCategoryDto>> GetAllAsync(PortfolioCategoryParameters portfolioCategoryParameters);
        Task<PortfolioCategoryDto> GetByIdAsync(int categoryId);
        Task<PortfolioCategoryDto> CreateAsync(CreatePortfolioCategoryDto createPortfolioCategoryDto);
        Task<PortfolioCategoryDto> UpdateAsync(int categoryId, UpdatePortfolioCategoryDto updatePortfolioCategoryDto);
        Task DeleteAsync(int categoryId);



    }
}