using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;
using Weblu.Application.Common.Interfaces;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IServiceRepository : IGenericRepository<Service, ServiceParameters>
    {
        Task LoadMethodsAsync(Service service);
        Task LoadFeaturesAsync(Service service);
        Task<Service?> GetByIdWithImagesAsync(int serviceId);
    }
}