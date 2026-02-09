using Weblu.Application.Dtos.Images.ImageDtos;
using Weblu.Application.Parameters.Images;

namespace Weblu.Application.Interfaces.Services.Images
{
    public interface IImageService
    {
        Task<List<ImageDto>> GetAllAsync(ImageParameters imageParameters);
        Task<ImageDto> GetByIdAsync(int imageId);
        Task<ImageDto> AddAsync(AddImageDto addImageDto);
        Task DeleteAsync(int imageId);
    }
}