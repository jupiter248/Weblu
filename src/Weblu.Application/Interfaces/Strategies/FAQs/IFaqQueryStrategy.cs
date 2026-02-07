using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.Faqs;

namespace Weblu.Application.Interfaces.Strategies.FAQs
{
    public interface IFaqQueryStrategy
    {
        IQueryable<Faq> Query(IQueryable<Faq> faqs, FaqParameters faqParameters);
    }
}