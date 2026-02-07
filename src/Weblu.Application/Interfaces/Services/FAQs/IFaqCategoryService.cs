using Weblu.Application.Dtos.FAQs.FaqCategoryDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.FAQs;

namespace Weblu.Application.Interfaces.Services.FAQs
{
    public interface IFaqCategoryService
    {
        Task<List<FaqCategoryDto>> GetAllFaqCategoriesAsync(FaqCategoryParameters faqCategoryParameters);
        Task<FaqCategoryDto> GetFaqCategoryByIdAsync(int faqCategoryId);
        Task<FaqCategoryDto> AddFaqCategoryAsync(AddFaqCategoryDto addFaqCategoryDto);
        Task<FaqCategoryDto> UpdateFaqCategoryAsync(int currentFaqCategoryId, UpdateFaqCategoryDto updateFaqCategoryDto);
        Task DeleteFaqCategoryAsync(int faqCategoryId);
    }   
}