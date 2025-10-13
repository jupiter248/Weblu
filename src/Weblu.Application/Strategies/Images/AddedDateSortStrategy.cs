using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Strategies.Images
{
    public class AddedDateSortStrategy : IImageQueryStrategy
    {
        public List<ImageMedia> Query(List<ImageMedia> images, ImageMediaParameters imageMediaParameters)
        {
            if (imageMediaParameters.AddedDateSort == CreatedDateSort.Newest)
            {
                return images.OrderByDescending(s => s.AddedAt).ToList();
            }
            else if (imageMediaParameters.AddedDateSort == CreatedDateSort.Oldest)
            {
                return images.OrderBy(s => s.AddedAt).ToList();
            }
            else
            {
                return images;
            }
        }
    }
}