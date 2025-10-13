using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Parameters;
using Weblu.Domain.Entities;



namespace Weblu.Application.Strategies.Images
{
    public class ImageQueryHandler
    {
        private IImageQueryStrategy _imageQueryStrategy;
        public ImageQueryHandler(IImageQueryStrategy imageQueryStrategy)
        {
            _imageQueryStrategy = imageQueryStrategy;
        }
        public List<ImageMedia> ExecuteServiceQuery(List<ImageMedia> images, ImageMediaParameters imageMediaParameters)
        {
            return _imageQueryStrategy.Query(images, imageMediaParameters);
        }
    }
}