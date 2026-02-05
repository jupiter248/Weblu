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
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;

namespace Weblu.Infrastructure.Repositories
{
    internal class ServiceRepository : GenericRepository<Service, ServiceParameters>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<PagedList<Service>> GetAllAsync(ServiceParameters serviceParameters)
        {
            IQueryable<Service> services = _context.Services.Where(a => !a.IsDeleted).Include(i => i.ServiceImages).ThenInclude(i => i.Image).AsNoTracking();
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

            return await PaginationExtensions<Service>.GetPagedList(services, serviceParameters.PageNumber, serviceParameters.PageSize);

        }

        public override async Task<Service?> GetByIdAsync(int serviceId)
        {
            Service? service = await _context.Services.Where(a => !a.IsDeleted).FirstOrDefaultAsync(s => s.Id == serviceId);
            return service;
        }

        public async Task<Service?> GetByIdWithImagesAsync(int serviceId)
        {
            Service? service = await _context.Services.Where(a => !a.IsDeleted).Include(i => i.ServiceImages).ThenInclude(i => i.Image).FirstOrDefaultAsync(s => s.Id == serviceId);
            return service;
        }

        public async Task LoadFeaturesAsync(Service service)
        {
            await _context.Entry(service).Collection(s => s.Features).LoadAsync();
        }

        public async Task LoadMethodsAsync(Service service)
        {
            await _context.Entry(service).Collection(s => s.Methods).LoadAsync();
        }
    }
}