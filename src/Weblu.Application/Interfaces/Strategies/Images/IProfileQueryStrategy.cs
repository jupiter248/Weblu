using Weblu.Application.Parameters.Images;
using Weblu.Domain.Entities.Media;

namespace Weblu.Application.Interfaces.Strategies.Images
{
    public interface IProfileQueryStrategy
    {
        IQueryable<ProfileMedia> Query(IQueryable<ProfileMedia> profiles, ProfileMediaParameters profileMediaParameters);
    }
}