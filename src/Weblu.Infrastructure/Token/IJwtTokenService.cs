using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Token
{
    public interface IJwtTokenService
    {
        public string GenerateAccessToken(AppUser user, IList<string> roles);
        public string GenerateRefreshToken();
    }
}