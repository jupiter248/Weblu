using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Strategies.Services;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Domain.Enums.Services.Parameters;

namespace Weblu.Infrastructure.Repositories
{
    internal class ServiceRepository : GenericRepository<Service, ServiceParameters>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IReadOnlyList<Service>> GetAllAsync(ServiceParameters serviceParameters)
        {
            IQueryable<Service> services = _context.Services.Include(i => i.ServiceImages).ThenInclude(i => i.Image).AsNoTracking();
            if (serviceParameters.PriceSort != PriceSort.All)
            {
                services = new ServiceQueryHandler(new PriceSortStrategy())
                .ExecuteServiceQuery(services, serviceParameters);
            }
            if (serviceParameters.DurationSort != DurationSort.All)
            {
                services = new ServiceQueryHandler(new DurationSortStrategy())
                .ExecuteServiceQuery(services, serviceParameters);
            }
            if (serviceParameters.CreatedDateSort != CreatedDateSort.All)
            {
                services = new ServiceQueryHandler(new CreatedDateSortStrategy())
                .ExecuteServiceQuery(services, serviceParameters);
            }

            return await services.ToListAsync();
        }

        public override async Task<Service?> GetByIdAsync(int serviceId)
        {
            Service? service = await _context.Services.Include(i => i.ServiceImages).ThenInclude(i => i.Image).FirstOrDefaultAsync(s => s.Id == serviceId);
            if (service == null)
            {
                return null;
            }
            return service;
        }
    }
}