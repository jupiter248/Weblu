using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.SocialMedia
{
    public class SocialMediaErrorCodes
    {
        public const string NotFound = "SOCIAL_MEDIA_NOT_FOUND";
        public const string NameRequired = "SOCIAL_MEDIA_NAME_REQUIRED";
        public const string NameMaxLength = "SOCIAL_MEDIA_NAME_MAX_LENGTH";
        public const string InvalidLink = "SOCIAL_MEDIA_INVALID_LINK";

    }
}