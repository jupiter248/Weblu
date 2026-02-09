using Weblu.Application.Interfaces.Strategies.Services;
using Weblu.Application.Parameters.Services;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Enums.Common.Parameters;

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