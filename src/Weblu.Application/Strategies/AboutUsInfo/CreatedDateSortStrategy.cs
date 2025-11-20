using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.About;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.AboutUsInfo
{
    public class CreatedDateSortStrategy : IAboutUsQueryStrategy
    {
        public IQueryable<AboutUs> Query(IQueryable<AboutUs> aboutUs, AboutUsParameters aboutUsParameters)
        {
            if (aboutUsParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return aboutUs.OrderByDescending(s => s.CreatedAt);
            }
            else if (aboutUsParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return aboutUs.OrderBy(s => s.CreatedAt);
            }
            else
            {
                return aboutUs;
            }
        }
    }
}