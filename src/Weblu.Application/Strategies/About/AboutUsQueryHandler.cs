using Weblu.Application.Interfaces.Strategies.AboutUsInformation;
using Weblu.Application.Parameters.About;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Strategies.About
{
    public class AboutUsQueryHandler
    {
        private readonly IAboutUsQueryStrategy _aboutUsQueryStrategy;
        public AboutUsQueryHandler(IAboutUsQueryStrategy aboutUsQueryStrategy)
        {
            _aboutUsQueryStrategy = aboutUsQueryStrategy;
        }
        public IQueryable<AboutUs> ExecuteAboutUsQuery(IQueryable<AboutUs> aboutUs, AboutUsParameters aboutUsParameters)
        {
            return _aboutUsQueryStrategy.Query(aboutUs, aboutUsParameters);
        }
    }
}