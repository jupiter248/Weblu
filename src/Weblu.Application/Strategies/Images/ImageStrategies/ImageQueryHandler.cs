using Weblu.Application.Interfaces.Strategies.Images;
using Weblu.Application.Parameters.Images;
using Weblu.Domain.Entities.Media;
namespace Weblu.Application.Strategies.Images.ImageStrategies
{
    public class ImageQueryHandler
    {
        private IImageQueryStrategy _imageQueryStrategy;
        public ImageQueryHandler(IImageQueryStrategy imageQueryStrategy)
        {
            _imageQueryStrategy = imageQueryStrategy;
        }
        public IQueryable<ImageMedia> ExecuteImageQuery(IQueryable<ImageMedia> images, ImageParameters imageParameters)
        {
            return _imageQueryStrategy.Query(images, imageParameters);
        }
    }
}