using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.Faqs;

namespace Weblu.Application.Interfaces.Repositories.FAQs
{
    public interface IFaqRepository :  IGenericRepository<Faq, FaqParameters>
    {
    }
}