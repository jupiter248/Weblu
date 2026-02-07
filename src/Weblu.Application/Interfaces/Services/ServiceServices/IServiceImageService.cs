using Weblu.Application.Dtos.Services.ServiceDtos.ServiceImageDtos;

namespace Weblu.Application.Interfaces.Services.ServiceServices
{
    public interface IServiceImageService
    {
        Task AddImageAsync(int serviceId, int imageId, AddServiceImageDto addServiceImageDto);
        Task DeleteImageAsync(int serviceId, int imageId);
    }
}