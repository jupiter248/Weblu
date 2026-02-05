using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Services.Common.Interfaces
{
    public interface IErrorService
    {
        string GetMessage(string errorCode);
    }
}