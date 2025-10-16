using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Media;



namespace Weblu.Application.Strategies.Images
{
    public class ImageQueryHandler
    {
        private IImageQueryStrategy _imageQueryStrategy;
        public ImageQueryHandler(IImageQueryStrategy imageQueryStrategy)
        {
            _imageQueryStrategy = imageQueryStrategy;
        }
        public List<ImageMedia> ExecuteServiceQuery(List<ImageMedia> images, ImageParameters imageParameters)
        {
            return _imageQueryStrategy.Query(images, imageParameters);
        }
    }
}