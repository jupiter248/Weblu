using Weblu.Application.Interfaces.Strategies.FAQs;
using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.Faqs;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Faqs
{
    public class CreatedDateSortStrategy : IFaqQueryStrategy
    {
        public IQueryable<Faq> Query(IQueryable<Faq> faqs, FaqParameters faqParameters)
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