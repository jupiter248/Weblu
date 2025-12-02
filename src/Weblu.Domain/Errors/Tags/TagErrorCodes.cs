using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Tags
{
    public class TagErrorCodes
    {
        public const string NotFound = "TAG_NOT_FOUND";
        public const string NameRequired = "TAG_NAME_REQUIRED";
        public const string NameMaxLength = "TAG_NAME_MAX_LENGTH";
        public const string DescriptionRequired = "TAG_DESCRIPTION_REQUIRED";
        public const string DescriptionMaxLength = "TAG_DESCRIPTION_MAX_LENGTH";
    }
}