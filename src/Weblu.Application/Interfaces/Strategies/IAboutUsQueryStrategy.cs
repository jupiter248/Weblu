using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IAboutUsQueryStrategy
    {
        IQueryable<AboutUs> Query(IQueryable<AboutUs> aboutUs, AboutUsParameters aboutUsParameters);
    }
}