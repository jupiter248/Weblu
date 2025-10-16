using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;

namespace Weblu.Application.Strategies.Services
{
    public class ServiceQueryHandler
    {
        private IServiceQueryStrategy _serviceQueryStrategy;
        public ServiceQueryHandler(IServiceQueryStrategy serviceQueryStrategy)
        {
            _serviceQueryStrategy = serviceQueryStrategy;
        }
        public List<Service> ExecuteServiceQuery(List<Service> services, ServiceParameters serviceParameters)
        {
            return _serviceQueryStrategy.Query(services, serviceParameters);
        }
    }
}