using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Enums.Services.Parameters;
using Weblu.Application.Parameters;

namespace Weblu.Application.Strategies.Services
{
    public class DurationSortStrategy : IServiceQueryStrategy
    {
        public IQueryable<Service> Query(IQueryable<Service> services, ServiceParameters serviceParameters)
        {
            if (serviceParameters.DurationSort == DurationSort.LongestDuration)
            {
                return services.OrderByDescending(s => s.BaseDurationInDays);
            }
            else if (serviceParameters.DurationSort == DurationSort.ShortestDuration)
            {
                return services.OrderBy(s => s.BaseDurationInDays);
            }
            else
            {
                return services;
            }
        }
    }
}