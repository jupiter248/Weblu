using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Faqs;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IFaqCategoryRepository
    {
        Task<IReadOnlyList<FaqCategory>> GetAllFaqCategoriesAsync();
        Task<FaqCategory?> GetFaqCategoryByIdAsync(int faqCategoryId);
        Task<bool> FaqCategoryExistsAsync(int faqCategoryId);
        Task AddFaqCategoryAsync(FaqCategory faqCategory);
        void UpdateFaqCategory(FaqCategory faqCategory);
        void DeleteFaqCategory(FaqCategory faqCategory);
    }
}