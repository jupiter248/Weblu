using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Strategies.AboutUsInfo
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