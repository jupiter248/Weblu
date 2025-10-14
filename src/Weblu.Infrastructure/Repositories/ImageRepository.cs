using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Strategies.Images;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Interfaces;
using Weblu.Domain.Parameters;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddImageAsync(ImageMedia image)
        {
            await _context.ImageMedia.AddAsync(image);
        }

        public void DeleteImage(ImageMedia image)
        {
            _context.ImageMedia.Remove(image);
        }

        public async Task<List<ImageMedia>> GetAllImagesAsync(ImageParameters imageParameters)
        {
            List<ImageMedia> imageMedia = await _context.ImageMedia.ToListAsync();

            var addedDateSort = new ImageQueryHandler(new AddedDateSortStrategy());
            imageMedia = addedDateSort.ExecuteServiceQuery(imageMedia, imageParameters);

            return imageMedia;
        }

        public async Task<ImageMedia?> GetImageItemByIdAsync(int imageId)
        {
            ImageMedia? imageMedia = await _context.ImageMedia.FirstOrDefaultAsync(i => i.Id == imageId);
            if (imageMedia == null)
            {
                return null;
            }
            return imageMedia;
        }

        public async Task<bool> ImageExistsAsync(int imageId)
        {
            bool imageMediaExists = await _context.ImageMedia.AnyAsync(i => i.Id == imageId);
            if (!imageMediaExists)
            {
                return false;
            }
            return true;
        }

        public void UpdateImage(ImageMedia image)
        {
            _context.ImageMedia.Update(image);
        }
    }
}