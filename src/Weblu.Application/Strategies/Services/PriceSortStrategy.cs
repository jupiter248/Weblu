using Weblu.Application.Interfaces.Strategies.Services;
using Weblu.Application.Parameters.Services;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Enums.Services.Parameters;

namespace Weblu.Application.Strategies.Services
{
    public class PriceSortStrategy : IServiceQueryStrategy
    {
        public IQueryable<Service> Query(IQueryable<Service> services, ServiceParameters serviceParameters)
        {
            if (serviceParameters.PriceSort == PriceSort.HighestPrice)
            {
                return services.OrderByDescending(s => s.BasePrice);
            }
            else if (serviceParameters.PriceSort == PriceSort.LowestPrice)
            {
                return services.OrderBy(s => s.BasePrice);
            }
            else
            {
                return services;
            }
        }
    }
}