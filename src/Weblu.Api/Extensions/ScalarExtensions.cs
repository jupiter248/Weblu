using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning.ApiExplorer;
using Scalar.AspNetCore;

namespace Weblu.Api.Extensions
{
    public static class ScalarExtensions
    {
        public static void UseScalarWithVersions(this ScalarOptions options)
        {
            options.Title = "My API Docs";
            options.WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");
        }
    }
}