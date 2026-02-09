using Weblu.Application.DTOs.FAQs.FAQCategoryDTOs;
using Weblu.Application.Parameters.FAQs;

namespace Weblu.Application.Interfaces.Services.FAQs
{
    public interface IFAQCategoryService
    {
        Task<List<FAQCategoryDTO>> GetAllAsync(FAQCategoryParameters fAQCategoryParameters);
        Task<FAQCategoryDTO> GetByIdAsync(int fAQCategoryId);
        Task<FAQCategoryDTO> CreateAsync(CreateFAQCategoryDTO createFAQCategoryDTO);
        Task<FAQCategoryDTO> UpdateAsync(int currentFAQCategoryId, UpdateFAQCategoryDTO updateFAQCategoryDTO);
        Task DeleteAsync(int fAQCategoryId);
    }
}