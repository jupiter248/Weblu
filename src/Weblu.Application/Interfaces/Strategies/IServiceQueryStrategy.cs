using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IServiceQueryStrategy
    {
        IQueryable<Service> Query(IQueryable<Service> services, ServiceParameters serviceParameters);
    }
}