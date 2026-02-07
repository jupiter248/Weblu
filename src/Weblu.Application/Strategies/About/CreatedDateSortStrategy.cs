using Weblu.Application.Interfaces.Strategies.AboutUsInformation;
using Weblu.Application.Parameters.About;
using Weblu.Domain.Entities.About;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.About
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