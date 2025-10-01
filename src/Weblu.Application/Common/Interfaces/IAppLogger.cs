using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Common.Interfaces
{
    public interface IAppLogger<T>
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}