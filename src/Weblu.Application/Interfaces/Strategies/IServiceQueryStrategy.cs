using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IServiceQueryStrategy
    {
        List<Service> Query(List<Service> services, ServiceParameters serviceParameters);
    }
}