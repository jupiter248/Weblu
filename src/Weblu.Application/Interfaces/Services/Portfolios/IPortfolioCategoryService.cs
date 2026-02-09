using Weblu.Application.DTOs.Portfolios.PortfolioCategoryDTOs;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Portfolios;

namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioCategoryService
    {
        Task<List<PortfolioCategoryDTO>> GetAllAsync(PortfolioCategoryParameters portfolioCategoryParameters);
        Task<PortfolioCategoryDTO> GetByIdAsync(int categoryId);
        Task<PortfolioCategoryDTO> CreateAsync(CreatePortfolioCategoryDTO createPortfolioCategoryDTO);
        Task<PortfolioCategoryDTO> UpdateAsync(int categoryId, UpdatePortfolioCategoryDTO updatePortfolioCategoryDTO);
        Task DeleteAsync(int categoryId);



    }
}