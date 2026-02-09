using Weblu.Application.Interfaces.Strategies.Images;
using Weblu.Application.Parameters.Images;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Images.ImageStrategies
{
    public class AddedDateSortStrategy : IImageQueryStrategy
    {
        public IQueryable<ImageMedia> Query(IQueryable<ImageMedia> images, ImageParameters imageParameters)
        {
            if (imageParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return images.OrderByDescending(s => s.CreatedAt);
            }
            else if (imageParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return images.OrderBy(s => s.CreatedAt);
            }
            else
            {
                return images;
            }
        }
    }
}