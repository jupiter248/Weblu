using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Application.Dtos.ServiceDtos.ServiceImageDtos;
using Weblu.Application.Parameters;

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