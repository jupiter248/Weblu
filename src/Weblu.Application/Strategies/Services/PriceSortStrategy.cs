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
    public class PriceSortStrategy : IServiceQueryStrategy
    {
        public List<Service> Query(List<Service> services, ServiceParameters serviceParameters)
        {
            if (serviceParameters.PriceSort == PriceSort.HighestPrice)
            {
                return services.OrderByDescending(s => s.BasePrice).ToList();
            }
            else if (serviceParameters.PriceSort == PriceSort.LowestPrice)
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