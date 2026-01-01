using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Api.Extensions
{
    public static class ConfigureLocalizations
    {
        public static void  AddLocalizations(this WebApplication app)
        {
            var supportedCultures = new[] { "en", "fa" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture("en")
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);
        }
    }
}