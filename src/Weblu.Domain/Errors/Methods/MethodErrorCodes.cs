using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Methods
{
    public class MethodErrorCodes
    {
        public const string MethodNotFound = "METHOD_NOT_FOUND";
        public const string MethodNameRequired = "METHOD_NAME_REQUIRED";
        public const string MethodNameMaxLength = "METHOD_NAME_MAX_LENGTH";
        public const string MethodDescriptionRequired = "METHOD_DESCRIPTION_REQUIRED";
        public const string MethodDescriptionMaxLength = "METHOD_DESCRIPTION_MAX_LENGTH";
    }
}