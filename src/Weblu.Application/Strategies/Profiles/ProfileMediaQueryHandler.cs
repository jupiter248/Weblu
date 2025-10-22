using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Media;

namespace Weblu.Application.Strategies.Profiles
{
    public class ProfileMediaQueryHandler
    {
        private IProfileQueryStrategy _profileMediaQueryStrategy;
        public ProfileMediaQueryHandler(IProfileQueryStrategy profileQueryStrategy)
        {
            _profileMediaQueryStrategy = profileQueryStrategy;
        }
        public List<ProfileMedia> ExecuteServiceQuery(List<ProfileMedia> images, ProfileMediaParameters imageParameters)
        {
            return _profileMediaQueryStrategy.Query(images, imageParameters);
        }
    }
}