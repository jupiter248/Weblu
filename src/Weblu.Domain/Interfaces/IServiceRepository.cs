using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Parameters;

namespace Weblu.Domain.Interfaces
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