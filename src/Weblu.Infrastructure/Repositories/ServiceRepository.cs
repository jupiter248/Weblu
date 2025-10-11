using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Strategies.Services;
using Weblu.Domain.Entities;
using Weblu.Domain.Interfaces;
using Weblu.Domain.Parameters;
using Weblu.Infrastructure.Data;

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

        public async Task<List<Service>> GetAllServicesAsync(ServiceParameters serviceParameters)
        {
            var services = await _context.Services.ToListAsync();

            var priceSortQuery = new ServiceQueryHandler(new PriceSortStrategy());
            services = priceSortQuery.ExecuteServiceQuery(services, serviceParameters);

            var durationSortQuery = new ServiceQueryHandler(new DurationSortStrategy());
            services = durationSortQuery.ExecuteServiceQuery(services, serviceParameters);

            var createdSortQuery = new ServiceQueryHandler(new CreatedDateSortStrategy());
            services = createdSortQuery.ExecuteServiceQuery(services, serviceParameters);

            return services;
        }

        public async Task<Service?> GetServiceByIdAsync(int serviceId)
        {
            Service? service = await _context.Services.Include(f => f.Features).Include(m => m.Methods).FirstOrDefaultAsync(s => s.Id == serviceId);
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