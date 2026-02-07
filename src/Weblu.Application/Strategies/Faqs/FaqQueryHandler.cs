using Weblu.Application.Interfaces.Strategies.FAQs;
using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.Faqs;

namespace Weblu.Application.Strategies.Faqs
{
    public class FaqQueryHandler
    {
        private readonly IFaqQueryStrategy _faqQueryStrategy;
        public FaqQueryHandler(IFaqQueryStrategy faqQueryStrategy)
        {
            _faqQueryStrategy = faqQueryStrategy;
        }
        public IQueryable<Faq> ExecuteFaqQuery(IQueryable<Faq> faqs, FaqParameters faqParameters)
        {
            return _faqQueryStrategy.Query(faqs, faqParameters);
        }
    }
}