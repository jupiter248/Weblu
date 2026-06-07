using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.FAQs;

namespace Weblu.Domain.Interfaces.Repositories.FAQs
{
    public interface IFAQRepository : IGenericRepository<FAQ, FAQParameters>
    {
    }
}