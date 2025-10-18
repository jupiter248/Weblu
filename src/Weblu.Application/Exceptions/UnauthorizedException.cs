using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Exceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string errorCode ) : base(errorCode, 401, null)
        {
        }
    }
}