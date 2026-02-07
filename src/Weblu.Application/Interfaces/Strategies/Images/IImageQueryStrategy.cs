using Weblu.Domain.Entities.Media;
using Weblu.Application.Parameters.Images;

namespace Weblu.Application.Interfaces.Strategies.Images
{
    public interface IImageQueryStrategy
    {
        IQueryable<ImageMedia> Query(IQueryable<ImageMedia> images, ImageParameters imageParameters);

    }
}