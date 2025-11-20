using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Auth
{
    public class AuthErrorCodes
    {
        public const string UsernameAlreadyUsed = "USERNAME_ALREADY_USED";
        public const string PhoneNumberAlreadyUsed = "PHONE_NUMBER_ALREADY_USED";
        public const string UserCreationFailed = "USER_CREATION_FAILED";
        public const string RoleAddingFailed = "ROLE_ADDING_FAILED";
        public const string IncorrectPassword = "INCORRECT_PASSWORD";
        ////
        public const string UsernameRequired = "USERNAME_REQUIRED";
        public const string UsernameMaxLength = "USERNAME_MAX_LENGTH";
        public const string UserFirstNameMaxLength = "USER_FIRST_NAME_MAX_LENGTH";
        public const string UserFirstNameRequired = "USER_FIRST_NAME_REQUIRED";
        public const string UserLastNameMaxLength = "USER_LAST_NAME_MAX_LENGTH";
        public const string UserLastNameRequired = "USER_LAST_NAME_REQUIRED";
        public const string PhoneRequired = "PHONE_REQUIRED";
        public const string InvalidPhone = "INVALID_PHONE";
        public const string EmailRequired = "EMAIL_REQUIRED";
        public const string InvalidEmail = "INVALID_EMAIL";

        public const string PasswordRequired = "PASSWORD_REQUIRED";
        public const string PasswordLength = "PASSWORD_LENGTH";

    }
}