using Weblu.Application.Interfaces.Strategies.FAQs;
using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.FAQs;

namespace Weblu.Application.Strategies.FAQs
{
    public class FAQQueryHandler
    {
        private readonly IFAQQueryStrategy _faqQueryStrategy;
        public FAQQueryHandler(IFAQQueryStrategy faqQueryStrategy)
        {
            _faqQueryStrategy = faqQueryStrategy;
        }
        public IQueryable<FAQ> ExecuteFAQQuery(IQueryable<FAQ> faqs, FAQParameters faqParameters)
        {
            return _faqQueryStrategy.Query(faqs, faqParameters);
        }
    }
}