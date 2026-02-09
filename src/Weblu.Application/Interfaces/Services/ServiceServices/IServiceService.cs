using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Services.ServiceDTOs;
using Weblu.Application.Parameters.Services;

namespace Weblu.Application.Interfaces.Services.ServiceServices
{
    public interface IServiceService
    {
        Task<List<ServiceSummaryDTO>> GetAllAsync(ServiceParameters serviceParameters);
        Task<PagedResponse<ServiceSummaryDTO>> GetAllPagedAsync(ServiceParameters serviceParameters);
        Task<ServiceDetailDTO> GetByIdAsync(int serviceId);
        Task<ServiceDetailDTO> CreateAsync(CreateServiceDTO createServiceDTO);
        Task<ServiceDetailDTO> UpdateAsync(int serviceId, UpdateServiceDTO updateServiceDTO);
        Task DeleteAsync(int serviceId);
        Task Publish(int portfolioId);
        Task Unpublish(int portfolioId);
    }
}