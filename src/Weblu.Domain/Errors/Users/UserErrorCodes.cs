using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Users
{
    public class UserErrorCodes
    {
        public const string UserNotFound = "USER_NOT_FOUND";
        public const string RoleNotFound = "ROLE_NOT_FOUND";
        public const string UserUpdateForbidden = "USER_UPDATE_FORBIDDEN";
        public const string UserDeleteForbidden = "USER_UPDATE_FORBIDDEN";
    }
}