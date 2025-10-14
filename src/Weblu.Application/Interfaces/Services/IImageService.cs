using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ImageDtos;
using Weblu.Domain.Entities;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Interfaces.Services
{
    public interface IImageService
    {
        Task<List<ImageDto>> GetAllImagesAsync(ImageParameters imageParameters);
        Task<ImageDto> GetImageByIdAsync(int imageId);
        Task<ImageDto> AddImageAsync(AddImageDto addImageDto);
        Task DeleteImageAsync(int imageId);
    }
}