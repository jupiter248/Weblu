using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Faqs;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class FaqCategoryRepository : IFaqCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public FaqCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddFaqCategoryAsync(FaqCategory faqCategory)
        {
            await _context.FaqCategories.AddAsync(faqCategory);
        }

        public void DeleteFaqCategory(FaqCategory faqCategory)
        {
            _context.FaqCategories.Remove(faqCategory);
        }

        public async Task<bool> FaqCategoryExistsAsync(int faqCategoryId)
        {
            bool faqCategoryExists = await _context.FaqCategories.AnyAsync(f => f.Id == faqCategoryId);
            return faqCategoryExists;
        }

        public async Task<List<FaqCategory>> GetAllFaqCategoriesAsync()
        {
            IQueryable<FaqCategory> faqCategories = _context.FaqCategories.AsQueryable();

            return await faqCategories.ToListAsync();
        }

        public async Task<FaqCategory?> GetFaqCategoryByIdAsync(int faqCategoryId)
        {
            FaqCategory? faqCategory = await _context.FaqCategories.FirstOrDefaultAsync(f => f.Id == faqCategoryId);
            if (faqCategory == null)
            {
                return null;
            }
            return faqCategory;
        }

        public void UpdateFaqCategory(FaqCategory faqCategory)
        {
            _context.FaqCategories.Update(faqCategory);
        }
    }
}