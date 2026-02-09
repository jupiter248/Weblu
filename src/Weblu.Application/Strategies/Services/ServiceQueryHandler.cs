using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters.Services;
using Weblu.Application.Interfaces.Strategies.Services;

namespace Weblu.Application.Strategies.Services
{
    public class ServiceQueryHandler
    {
        private IServiceQueryStrategy _serviceQueryStrategy;
        public ServiceQueryHandler(IServiceQueryStrategy serviceQueryStrategy)
        {
            _serviceQueryStrategy = serviceQueryStrategy;
        }
        public IQueryable<Service> ExecuteServiceQuery(IQueryable<Service> services, ServiceParameters serviceParameters)
        {
            return _serviceQueryStrategy.Query(services, serviceParameters);
        }
    }
}