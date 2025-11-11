using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Faqs;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class FaqRepository : IFaqRepository
    {
        private readonly ApplicationDbContext _context;
        public FaqRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddFaqAsync(Faq faq)
        {
            await _context.Faqs.AddAsync(faq);
        }

        public void DeleteFaq(Faq faq)
        {
            _context.Faqs.Remove(faq);
        }

        public async Task<bool> FaqExistsAsync(int faqId)
        {
            bool faqExists = await _context.Faqs.AnyAsync(f => f.Id == faqId);
            return faqExists;
        }

        public async Task<List<Faq>> GetAllFaqsAsync(FaqParameters faqParameters)
        {
            IQueryable<Faq> faqs = _context.Faqs.AsQueryable();

            return await faqs.ToListAsync();
        }

        public async Task<Faq?> GetFaqByIdAsync(int faqId)
        {
            Faq? faq = await _context.Faqs.FirstOrDefaultAsync(f => f.Id == faqId);
            if (faq == null)
            {
                return null;
            }
            return faq;
        }

        public void UpdateFaq(Faq faq)
        {
            _context.Faqs.Update(faq);
        }
    }
}