using Weblu.Domain.Entities.FAQs;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Parameters.FAQs;
using Weblu.Application.Interfaces.Repositories.FAQs;

namespace Weblu.Infrastructure.Repositories
{
    internal class FAQCategoryRepository : GenericRepository<FAQCategory, FAQCategoryParameters>, IFAQCategoryRepository
    {
        public FAQCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}