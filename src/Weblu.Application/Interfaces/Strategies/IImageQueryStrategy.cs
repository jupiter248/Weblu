using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Media;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IImageQueryStrategy
    {
        List<ImageMedia> Query(List<ImageMedia> images, ImageParameters imageParameters);

    }
}