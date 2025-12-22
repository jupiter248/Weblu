using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Users;

namespace Weblu.Infrastructure.UnitTests.Helpers
{
    public class ClaimsPrincipalTestHelper
    {
        public static ClaimsPrincipal CreateUser(
                    string userId,
                    UserType role = UserType.User)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            return new ClaimsPrincipal(identity);
        }
    }
}