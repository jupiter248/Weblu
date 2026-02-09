using Weblu.Application.Interfaces.Strategies.Images;
using Weblu.Application.Parameters.Images;
using Weblu.Domain.Entities.Media;

namespace Weblu.Application.Strategies.Images.Profiles
{
    public class ProfileMediaQueryHandler
    {
        private IProfileQueryStrategy _profileMediaQueryStrategy;
        public ProfileMediaQueryHandler(IProfileQueryStrategy profileQueryStrategy)
        {
            _profileMediaQueryStrategy = profileQueryStrategy;
        }
        public IQueryable<ProfileMedia> ExecuteProfileQuery(IQueryable<ProfileMedia> images, ProfileMediaParameters imageParameters)
        {
            return _profileMediaQueryStrategy.Query(images, imageParameters);
        }
    }
}