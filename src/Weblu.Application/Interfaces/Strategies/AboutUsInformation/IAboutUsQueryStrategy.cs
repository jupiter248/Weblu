using Weblu.Application.Parameters.About;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Interfaces.Strategies.AboutUsInformation
{
    public interface IAboutUsQueryStrategy
    {
        IQueryable<AboutUs> Query(IQueryable<AboutUs> aboutUs, AboutUsParameters aboutUsParameters);
    }
}