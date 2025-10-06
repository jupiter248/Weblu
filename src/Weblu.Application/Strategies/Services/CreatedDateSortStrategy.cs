using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Strategies.Services
{
    public class CreatedDateSortStrategy : IServiceQueryStrategy
    {
        public List<Service> Query(List<Service> services, ServiceParameters serviceParameters)
        {
            if (serviceParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return services.OrderByDescending(s => s.CreatedAt).ToList();
            }
            else if (serviceParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return services.OrderBy(s => s.CreatedAt).ToList();
            }
            else
            {
                return services;    
            }
        }
    }
}