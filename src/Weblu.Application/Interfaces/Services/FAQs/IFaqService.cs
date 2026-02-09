using Weblu.Application.DTOs.FAQs.FAQDTOs;
using Weblu.Application.Parameters.FAQs;

namespace Weblu.Application.Interfaces.Services.FAQs
{
    public interface IFAQService
    {
        Task<List<FAQDTO>> GetAllAsync(FAQParameters fAQParameters);
        Task<FAQDTO> GetByIdAsync(int fAQId);
        Task<FAQDTO> CreateAsync(CreateFAQDTO createFAQDTO);
        Task<FAQDTO> UpdateAsync(int currentFAQId, UpdateFAQDTO updateFAQDTO);
        Task DeleteAsync(int fAQId);
        Task Publish(int fAQId);
        Task Unpublish(int fAQId);
    }
}