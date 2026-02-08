using Microsoft.EntityFrameworkCore;
using Weblu.Application.Strategies.FAQs;
using Weblu.Domain.Entities.FAQs;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;
using Weblu.Application.Parameters.FAQs;
using Weblu.Application.Interfaces.Repositories.FAQs;

namespace Weblu.Infrastructure.Repositories.FAQs
{
    internal class FAQRepository : GenericRepository<FAQ, FAQParameters>, IFAQRepository
    {
        public FAQRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<PagedList<FAQ>> GetAllAsync(FAQParameters faqParameters)
        {
            IQueryable<FAQ> faqs = _context.FAQs.Where(a => !a.IsDeleted).Include(c => c.Category).AsNoTracking();

            if (faqParameters.CreatedDateSort != CreatedDateSort.All)
            {
                faqs = new FAQQueryHandler(new CreatedDateSortStrategy())
                .ExecuteFAQQuery(faqs, faqParameters);
            }

            return await PaginationExtensions<FAQ>.GetPagedList(faqs, faqParameters.PageNumber, faqParameters.PageSize);
        }

        public override async Task<FAQ?> GetByIdAsync(int faqId)
        {
            FAQ? faq = await _context.FAQs.Where(a => !a.IsDeleted).Include(c => c.Category).FirstOrDefaultAsync(f => f.Id == faqId);
            if (faq == null)
            {
                return null;
            }
            return faq;
        }
    }
}