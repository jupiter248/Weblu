using Weblu.Application.DTOs.Images.ImageDTOs;
using Weblu.Application.Parameters.Images;

namespace Weblu.Application.Interfaces.Services.Images
{
    public interface IImageService
    {
        Task<List<ImageDTO>> GetAllAsync(ImageParameters imageParameters);
        Task<ImageDTO> GetByIdAsync(int imageId);
        Task<ImageDTO> AddAsync(AddImageDTO addImageDTO);
        Task DeleteAsync(int imageId);
    }
}