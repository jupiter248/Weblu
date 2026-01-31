using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Weblu.Application.Common.Interfaces;

namespace Weblu.Infrastructure.Common.Services
{
    public class FilePathProvider : IFilePathProvider
    {
        private readonly IWebHostEnvironment _env;

        public FilePathProvider(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string GetWebRootPath() => _env.WebRootPath;
    }
}