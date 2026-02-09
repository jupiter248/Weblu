using Weblu.Application.Dtos.Services.ServiceDtos.ServiceImageDtos;

namespace Weblu.Application.Interfaces.Services.ServiceServices
{
    public interface IServiceImageService
    {
        Task AddAsync(int serviceId, int imageId, AddServiceImageDto addServiceImageDto);
        Task DeleteAsync(int serviceId, int imageId);
    }
}