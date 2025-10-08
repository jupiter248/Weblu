using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Interfaces.Services
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetAllServicesAsync(ServiceParameters serviceParameters);
        Task<ServiceDto> GetServiceByIdAsync(int serviceId);
        Task<ServiceDto> AddServiceAsync(AddServiceDto addServiceDto);
        Task<ServiceDto> UpdateServiceAsync(int serviceId, UpdateServiceDto updateServiceDto);
        Task DeleteServiceAsync(int serviceId);
        // Features
        Task AddFeatureToServiceAsync(int serviceId, int featureId);
        Task DeleteFeatureFromServiceAsync(int serviceId, int featureId);
    }
}