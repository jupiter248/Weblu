using Microsoft.EntityFrameworkCore;
using Weblu.Domain.Entities.Media;
using Weblu.Infrastructure.Data;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;
using Weblu.Application.Parameters.Images;
using Weblu.Application.Interfaces.Repositories.Images;
using Weblu.Application.Strategies.Images.ImageStrategies;

namespace Weblu.Infrastructure.Repositories.Images
{
    internal class ImageRepository : GenericRepository<ImageMedia, ImageParameters>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<PagedList<ImageMedia>> GetAllAsync(ImageParameters imageParameters)
        {
            IQueryable<ImageMedia> imageMedia = _context.ImageMedia.AsNoTracking();

            if (imageParameters.CreatedDateSort != CreatedDateSort.All)
            {
                imageMedia = new ImageQueryHandler(new AddedDateSortStrategy())
                .ExecuteImageQuery(imageMedia, imageParameters);
            }

            return await PaginationExtensions<ImageMedia>.GetPagedList(imageMedia, imageParameters.PageNumber, imageParameters.PageSize);
        }
    }
}