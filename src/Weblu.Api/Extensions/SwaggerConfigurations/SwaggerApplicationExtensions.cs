using Asp.Versioning.ApiExplorer;

namespace Weblu.Api.Extensions.SwaggerConfigurations
{
    public static class SwaggerApplicationExtensions
    {
        public static IApplicationBuilder UseSwaggerWithVersions(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices
                .GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();

            // Make an API selector for different versions
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant() // V1, V2
                    );
                }
            });

            return app;
        }
    }
}
