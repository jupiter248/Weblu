using Weblu.Application.Interfaces.Strategies.FAQs;
using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.FAQs;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.FAQs
{
    public class CreatedDateSortStrategy : IFAQQueryStrategy
    {
        public IQueryable<FAQ> Query(IQueryable<FAQ> faqs, FAQParameters faqParameters)
        {
            if (faqParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return faqs.OrderByDescending(c => c.CreatedAt);
            }
            else if (faqParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return faqs.OrderBy(c => c.CreatedAt);
            }
            else
            {
                return faqs;
            }
        }
    }
}