using Weblu.Application.Dtos.Images.ImageDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Images;

namespace Weblu.Application.Interfaces.Services.Images
{
    public interface IImageService
    {
        Task<List<ImageDto>> GetAllImagesAsync(ImageParameters imageParameters);
        Task<ImageDto> GetImageByIdAsync(int imageId);
        Task<ImageDto> AddImageAsync(AddImageDto addImageDto);
        Task DeleteImageAsync(int imageId);
    }
}