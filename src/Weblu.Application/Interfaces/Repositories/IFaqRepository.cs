using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Faqs;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IFaqRepository
    {
        Task<IReadOnlyList<Faq>> GetAllFaqsAsync(FaqParameters faqParameters);
        Task<Faq?> GetFaqByIdAsync(int faqId);
        Task<bool> FaqExistsAsync(int faqId);
        Task AddFaqAsync(Faq faq);
        void UpdateFaq(Faq faq);
        void DeleteFaq(Faq faq);
    }
}