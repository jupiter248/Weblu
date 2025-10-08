using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Exceptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string errorCode) : base(errorCode, 409, null)
        {
        }
    }
}