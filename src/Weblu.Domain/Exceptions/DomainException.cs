using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public string ErrorCode { get; set; } = default!;
        public int StatusCode { get; set; } = default!;

        public DomainException(string errorCode, int statusCode)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}