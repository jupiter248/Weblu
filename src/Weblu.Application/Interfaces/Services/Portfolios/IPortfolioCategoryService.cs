using Weblu.Application.Dtos.Portfolios.PortfolioCategory;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Portfolios;

namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioCategoryService
    {
        Task<List<PortfolioCategoryDto>> GetAllPortfolioCategoriesAsync(PortfolioCategoryParameters portfolioCategoryParameters);
        Task<PortfolioCategoryDto> GetPortfolioCategoryByIdAsync(int categoryId);
        Task<PortfolioCategoryDto> AddPortfolioCategoryAsync(AddPortfolioCategoryDto addPortfolioCategoryDto);
        Task<PortfolioCategoryDto> UpdatePortfolioCategoryAsync(int categoryId, UpdatePortfolioCategoryDto updatePortfolioCategoryDto);
        Task DeletePortfolioCategoryAsync(int categoryId);



    }
}