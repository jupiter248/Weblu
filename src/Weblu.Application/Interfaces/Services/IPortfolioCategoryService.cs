using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.PortfolioCategory;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Interfaces.Services
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