using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Faqs;
using Weblu.Domain.Entities.Faqs;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;

namespace Weblu.Infrastructure.Repositories
{
    internal class FaqRepository : GenericRepository<Faq, FaqParameters>, IFaqRepository
    {
        public FaqRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<PagedList<Faq>> GetAllAsync(FaqParameters faqParameters)
        {
            IQueryable<Faq> faqs = _context.Faqs.Where(a => !a.IsDeleted).Include(c => c.Category).AsNoTracking();

            if (faqParameters.CreatedDateSort != CreatedDateSort.All)
            {
                faqs = new FaqQueryHandler(new CreatedDateSortStrategy())
                .ExecuteFaqQuery(faqs, faqParameters);
            }

            return await PaginationExtensions<Faq>.GetPagedList(faqs, faqParameters.PageNumber, faqParameters.PageSize);
        }

        public override async Task<Faq?> GetByIdAsync(int faqId)
        {
            Faq? faq = await _context.Faqs.Where(a => !a.IsDeleted).Include(c => c.Category).FirstOrDefaultAsync(f => f.Id == faqId);
            if (faq == null)
            {
                return null;
            }
            return faq;
        }
    }
}