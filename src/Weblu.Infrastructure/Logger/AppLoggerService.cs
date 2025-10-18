using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Weblu.Application.Common.Interfaces;

namespace Weblu.Infrastructure.Logger
{
    public class AppLoggerService<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public AppLoggerService(ILogger<T> logger)
        {
            _logger = logger;
        }
        public void LogError(string message)
        {
            _logger.LogError(message);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }
    }
}