using Weblu.Application.Dtos.FAQs.FaqDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.FAQs;

namespace Weblu.Application.Interfaces.Services.FAQs
{
    public interface IFaqService
    {
        Task<List<FaqDto>> GetAllFaqsAsync(FaqParameters faqParameters);
        Task<FaqDto> GetFaqByIdAsync(int faqId);
        Task<FaqDto> AddFaqAsync(AddFaqDto addFaqDto);
        Task<FaqDto> UpdateFaqAsync(int currentFaqId, UpdateFaqDto updateFaqDto);
        Task DeleteFaqAsync(int faqId);
    }
}