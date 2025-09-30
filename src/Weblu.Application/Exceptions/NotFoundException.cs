using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message, string errorCode) : base(message, errorCode, 404, null)
        {
        }
    }
}