using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Exceptions
{
    public class ValidationException : AppException
    {
        public ValidationException( string errorCode, List<string>? details) : base(errorCode, 422, details)
        {
        }
    }
}