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
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;
        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddServiceAsync(Service service)
        {
            await _context.Services.AddAsync(service);
        }

        public void DeleteService(Service service)
        {
            _context.Services.Remove(service);
        }

        public async Task<IReadOnlyList<Service>> GetAllServicesAsync(ServiceParameters serviceParameters)
        {
            IQueryable<Service> services = _context.Services.Include(i => i.ServiceImages).ThenInclude(i => i.Image);
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

        public async Task<Service?> GetServiceByIdAsync(int serviceId)
        {
            Service? service = await _context.Services.Include(i => i.ServiceImages).ThenInclude(i => i.Image).Include(f => f.Features).Include(m => m.Methods).FirstOrDefaultAsync(s => s.Id == serviceId);
            if (service == null)
            {
                return null;
            }
            return service;
        }

        public async Task<bool> ServiceExistsAsync(int serviceId)
        {
            bool serviceExists = await _context.Services.AnyAsync(s => s.Id == serviceId);
            return serviceExists;
        }

        public void UpdateService(Service service)
        {
            _context.Services.Update(service);
        }
    }
}