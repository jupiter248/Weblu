using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string errorCode) : base(errorCode, 404, null)
        {
        }
    }
}