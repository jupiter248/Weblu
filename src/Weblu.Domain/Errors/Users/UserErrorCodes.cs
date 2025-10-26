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
        public const string UserDeleteForbidden = "USER_DELETE_FORBIDDEN";
        public const string UserChangePasswordFailed = "USER_CHANGE_PASSWORD_FAILED";
        public const string OldPasswordIsIncorrect = "OLD_PASSWORD_IS_INCORRECT";
        public const string OldPasswordRequired = "OLD_PASSWORD_REQUIRED";
        public const string OldPasswordLength = "OLD_PASSWORD_LENGTH";
        public const string NewPasswordRequired = "NEW_PASSWORD_REQUIRED";
        public const string NewPasswordLength = "NEW_PASSWORD_LENGTH";
        public const string UserAlreadyAddedMainProfileImage = "USER_ALREADY_ADDED_MAIN_PROFILE_IMAGE";
    }
}