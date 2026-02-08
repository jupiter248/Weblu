using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Services.ServiceDtos;
using Weblu.Application.Parameters.Services;

namespace Weblu.Application.Interfaces.Services.ServiceServices
{
    public interface IServiceService
    {
        Task<List<ServiceSummaryDto>> GetAllAsync(ServiceParameters serviceParameters);
        Task<PagedResponse<ServiceSummaryDto>> GetAllPagedAsync(ServiceParameters serviceParameters);
        Task<ServiceDetailDto> GetByIdAsync(int serviceId);
        Task<ServiceDetailDto> CreateAsync(CreateServiceDto createServiceDto);
        Task<ServiceDetailDto> UpdateAsync(int serviceId, UpdateServiceDto updateServiceDto);
        Task DeleteAsync(int serviceId);
    }
}