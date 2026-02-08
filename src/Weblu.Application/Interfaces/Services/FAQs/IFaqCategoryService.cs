using Weblu.Application.Dtos.FAQs.FAQCategoryDtos;
using Weblu.Application.Parameters.FAQs;

namespace Weblu.Application.Interfaces.Services.FAQs
{
    public interface IFAQCategoryService
    {
        Task<List<FAQCategoryDto>> GetAllAsync(FAQCategoryParameters faqCategoryParameters);
        Task<FAQCategoryDto> GetByIdAsync(int faqCategoryId);
        Task<FAQCategoryDto> CreateAsync(CreateFAQCategoryDto createFAQCategoryDto);
        Task<FAQCategoryDto> UpdateAsync(int currentFAQCategoryId, UpdateFAQCategoryDto updateFAQCategoryDto);
        Task DeleteAsync(int faqCategoryId);
    }
}