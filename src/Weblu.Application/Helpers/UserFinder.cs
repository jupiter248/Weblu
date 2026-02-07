using System.Security.Claims;

namespace Weblu.Application.Helpers
{
    public static class UserFinder
    {
        public static string? GetUserId(this ClaimsPrincipal user)
        {
            // get the authorize user  
            string reference = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            string? username = user.Claims.FirstOrDefault(x => x.Type == reference)?.Value;
            if (username != null)
            {
                return username;
            }
            else
            {
                return null;
            }
        }
    }
}