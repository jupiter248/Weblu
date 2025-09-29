using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Enums.Services.Parameters;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Strategies.Services
{
    public class DurationSortStrategy : IServiceQueryStrategy
    {
        public List<Service> Query(List<Service> services, ServiceParameters serviceParameters)
        {
            if (serviceParameters.DurationSort == DurationSort.LongestDuration)
            {
                return services.OrderByDescending(s => s.BasePrice).ToList();
            }
            else if (serviceParameters.DurationSort == DurationSort.ShortestDuration)
            {
                return services.OrderBy(s => s.BasePrice).ToList();
            }
            else
            {
                return [];
            }
        }
    }
}