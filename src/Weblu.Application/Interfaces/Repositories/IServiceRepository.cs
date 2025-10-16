using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllServicesAsync(ServiceParameters serviceParameters);
        Task<Service?> GetServiceByIdAsync(int serviceId);
        Task<bool> ServiceExistsAsync(int serviceId);
        Task AddServiceAsync(Service service);
        void UpdateService(Service service);
        void DeleteService(Service service);



    }
}