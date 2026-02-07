using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.About;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Interfaces.Repositories.About
{
    public interface IAboutUsRepository : IGenericRepository<AboutUs, AboutUsParameters>
    {
    }
}