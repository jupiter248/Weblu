using Weblu.Application.DTOs.Services.ServiceDTOs.ServiceImageDTOs;

namespace Weblu.Application.Interfaces.Services.ServiceServices
{
    public interface IServiceImageService
    {
        Task AddAsync(int serviceId, int imageId, AddServiceImageDTO addServiceImageDTO);
        Task DeleteAsync(int serviceId, int imageId);
    }
}