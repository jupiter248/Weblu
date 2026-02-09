using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters.Services;

namespace Weblu.Application.Interfaces.Strategies.Services
{
    public interface IServiceQueryStrategy
    {
        IQueryable<Service> Query(IQueryable<Service> services, ServiceParameters serviceParameters);
    }
}