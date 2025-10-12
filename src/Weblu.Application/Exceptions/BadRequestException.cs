using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string errorCode) : base(errorCode, 400, null)
        {
        }
    }
}