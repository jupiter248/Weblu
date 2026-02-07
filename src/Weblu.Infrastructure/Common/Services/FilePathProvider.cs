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