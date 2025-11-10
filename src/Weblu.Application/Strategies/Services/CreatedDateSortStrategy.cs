using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Application.Parameters;

namespace Weblu.Application.Strategies.Services
{
    public class CreatedDateSortStrategy : IServiceQueryStrategy
    {
        public IQueryable<Service> Query(IQueryable<Service> services, ServiceParameters serviceParameters)
        {
            if (serviceParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return services.OrderByDescending(s => s.CreatedAt);
            }
            else if (serviceParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return services.OrderBy(s => s.CreatedAt);
            }
            else
            {
                return services;    
            }
        }
    }
}