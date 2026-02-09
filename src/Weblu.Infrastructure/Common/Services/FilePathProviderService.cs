using Microsoft.AspNetCore.Hosting;
using Weblu.Application.Common.Interfaces;

namespace Weblu.Infrastructure.Common.Services
{
    public class FilePathProviderService : IFilePathProviderService
    {
        private readonly IWebHostEnvironment _env;

        public FilePathProviderService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string GetWebRootPath() => _env.WebRootPath;
    }
}