using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.FAQs;

namespace Weblu.Application.Interfaces.Strategies.FAQs
{
    public interface IFAQQueryStrategy
    {
        IQueryable<FAQ> Query(IQueryable<FAQ> faqs, FAQParameters fAQParameters);
    }
}