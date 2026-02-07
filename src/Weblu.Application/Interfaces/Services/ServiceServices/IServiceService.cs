using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Services.ServiceDtos;
using Weblu.Application.Parameters.Services;

namespace Weblu.Application.Interfaces.Services.ServiceServices
{
    public interface IServiceService
    {
        Task<List<ServiceSummaryDto>> GetAllServicesAsync(ServiceParameters serviceParameters);
        Task<PagedResponse<ServiceSummaryDto>> GetAllPagedServiceAsync(ServiceParameters serviceParameters);
        Task<ServiceDetailDto> GetServiceByIdAsync(int serviceId);
        Task<ServiceDetailDto> AddServiceAsync(AddServiceDto addServiceDto);
        Task<ServiceDetailDto> UpdateServiceAsync(int serviceId, UpdateServiceDto updateServiceDto);
        Task DeleteServiceAsync(int serviceId);
    }
}