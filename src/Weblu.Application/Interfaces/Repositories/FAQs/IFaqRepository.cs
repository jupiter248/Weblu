using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.FAQs;

namespace Weblu.Application.Interfaces.Repositories.FAQs
{
    public interface IFAQRepository : IGenericRepository<FAQ, FAQParameters>
    {
    }
}