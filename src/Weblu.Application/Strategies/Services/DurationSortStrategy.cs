using Weblu.Domain.Entities.Services;
using Weblu.Domain.Enums.Services.Parameters;
using Weblu.Application.Parameters.Services;
using Weblu.Application.Interfaces.Strategies.Services;

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