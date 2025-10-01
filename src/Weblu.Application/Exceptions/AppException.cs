using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; set; }
        public string ErrorCode { get; set; } = string.Empty;
        public List<string>? Details { get; set; } = [];

        public AppException( string errorCode, int statusCode, List<string>? details)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
            Details = details;
        }
    }
}