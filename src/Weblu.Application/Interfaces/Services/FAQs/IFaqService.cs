using Weblu.Application.Dtos.FAQs.FAQDtos;
using Weblu.Application.Parameters.FAQs;

namespace Weblu.Application.Interfaces.Services.FAQs
{
    public interface IFAQService
    {
        Task<List<FAQDto>> GetAllAsync(FAQParameters faqParameters);
        Task<FAQDto> GetByIdAsync(int faqId);
        Task<FAQDto> CreateAsync(CreateFAQDto createFAQDto);
        Task<FAQDto> UpdateAsync(int currentFAQId, UpdateFAQDto updateFAQDto);
        Task DeleteAsync(int faqId);
    }
}