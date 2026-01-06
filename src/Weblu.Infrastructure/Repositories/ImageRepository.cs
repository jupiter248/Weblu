using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Strategies.Images;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Media;
using Weblu.Application.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;

namespace Weblu.Infrastructure.Repositories
{
    internal class ImageRepository : GenericRepository<ImageMedia, ImageParameters>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IReadOnlyList<ImageMedia>> GetAllAsync(ImageParameters imageParameters)
        {
            IQueryable<ImageMedia> imageMedia = _context.ImageMedia.AsNoTracking();

            if (imageParameters.CreatedDateSort != CreatedDateSort.All)
            {
                imageMedia = new ImageQueryHandler(new AddedDateSortStrategy())
                .ExecuteImageQuery(imageMedia, imageParameters);
            }

            return await PagedList<ImageMedia>.GetPagedList(imageMedia, imageParameters.PageNumber, imageParameters.PageSize);
        }
    }
}