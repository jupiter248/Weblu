using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Application.Parameters;

namespace Weblu.Application.Strategies.Images
{
    public class AddedDateSortStrategy : IImageQueryStrategy
    {
        public IQueryable<ImageMedia> Query(IQueryable<ImageMedia> images, ImageParameters imageParameters)
        {
            if (imageParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return images.OrderByDescending(s => s.AddedAt);
            }
            else if (imageParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return images.OrderBy(s => s.AddedAt);
            }
            else
            {
                return images;
            }
        }
    }
}