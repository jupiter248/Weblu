using Weblu.Domain.Entities.Faqs;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Parameters.FAQs;
using Weblu.Application.Interfaces.Repositories.FAQs;

namespace Weblu.Infrastructure.Repositories
{
    internal class FaqCategoryRepository : GenericRepository<FaqCategory , FaqCategoryParameters>, IFaqCategoryRepository
    {
        public FaqCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}