using System.Globalization;
using System.Resources;
using Weblu.Application.Services.Common.Interfaces;

namespace Weblu.Infrastructure.Localization
{
    public class ErrorService : IErrorService
    {
        private readonly ResourceManager _resourceManager;

        public ErrorService()
        {
            // Load the resource file "Errors"
            _resourceManager = new ResourceManager(
                "Weblu.Infrastructure.Localization.Resources.Errors",
                typeof(ErrorService).Assembly
            );
        }
        public string GetMessage(string code)
        {
            var culture = CultureInfo.CurrentUICulture;
            var value = _resourceManager.GetString(code, culture);
            return string.IsNullOrEmpty(value) ? code : value;
        }
    }
}