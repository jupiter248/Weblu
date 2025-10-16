using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Application.Dtos.ServiceDtos.ServiceImageDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services
{
    public interface IServiceService
    {
        Task<List<ServiceSummaryDto>> GetAllServicesAsync(ServiceParameters serviceParameters);
        Task<ServiceDetailDto> GetServiceByIdAsync(int serviceId);
        Task<ServiceDetailDto> AddServiceAsync(AddServiceDto addServiceDto);
        Task<ServiceDetailDto> UpdateServiceAsync(int serviceId, UpdateServiceDto updateServiceDto);
        Task DeleteServiceAsync(int serviceId);
        // Features
        Task AddFeatureToServiceAsync(int serviceId, int featureId);
        Task DeleteFeatureFromServiceAsync(int serviceId, int featureId);
        // Methods
        Task AddMethodToService(int serviceId, int methodId);
        Task DeleteMethodFromService(int serviceId, int methodId);
        // Images
        Task AddImageToService(int serviceId, int imageId, AddServiceImageDto addServiceImageDto);
        Task DeleteImageToService(int serviceId, int imageId);

    }
}