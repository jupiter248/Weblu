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
            if (imageParameters.AddedDateSort == CreatedDateSort.Newest)
            {
                return images.OrderByDescending(s => s.AddedAt);
            }
            else if (imageParameters.AddedDateSort == CreatedDateSort.Oldest)
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