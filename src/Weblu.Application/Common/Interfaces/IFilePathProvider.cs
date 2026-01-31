using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Common.Interfaces
{
    public interface IFilePathProvider
    {
        public string GetWebRootPath();
    }
}