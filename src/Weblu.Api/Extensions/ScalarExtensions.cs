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
        public static void UseScalarWithVersions(this ScalarOptions options, IApplicationBuilder app)
        {
            var provider = app.ApplicationServices
            .GetRequiredService<IApiVersionDescriptionProvider>();
            var documents = new List<ScalarDocument>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                documents.Add(new ScalarDocument(description.GroupName, $"API {description.GroupName}", $"swagger/{description.GroupName}/swagger.json"));
            }
            documents.Add(new ScalarDocument("Galaxy", "Galaxy API", "https://registry.scalar.com/@scalar/apis/galaxy?format=json"));
            options.AddDocuments(documents);
        }
    }
}