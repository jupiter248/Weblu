using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Media;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<List<ImageMedia>> GetAllImagesAsync(ImageParameters imageParameters);
        Task<ImageMedia?> GetImageItemByIdAsync(int imageId);
        Task<bool> ImageExistsAsync(int imageId);
        Task AddImageAsync(ImageMedia image);
        void UpdateImage(ImageMedia image);
        void DeleteImage(ImageMedia image);
    }
}