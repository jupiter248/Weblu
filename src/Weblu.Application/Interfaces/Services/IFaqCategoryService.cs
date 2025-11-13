using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.FaqCategoryDtos;
using Weblu.Domain.Entities.Faqs;

namespace Weblu.Application.Interfaces.Services
{
    public interface IFaqCategoryService
    {
        Task<List<FaqCategoryDto>> GetAllFaqCategoriesAsync();
        Task<FaqCategoryDto> GetFaqCategoryByIdAsync(int faqCategoryId);
        Task<FaqCategoryDto> AddFaqCategoryAsync(AddFaqCategoryDto addFaqCategoryDto);
        Task<FaqCategoryDto> UpdateFaqCategoryAsync(int currentFaqCategoryId, UpdateFaqCategoryDto updateFaqCategoryDto);
        Task DeleteFaqCategoryAsync(int faqCategoryId);
    }
}