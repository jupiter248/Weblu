
namespace Weblu.Api.Extensions
{
    public static class ConfigureCors
    {
        public static void ApplyCors(this IServiceCollection services)
        {
            services.AddCors(opt =>
            opt.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .WithMethods(
                        "POST",
                        "PUT",
                        "DELETE",
                        "GET"
                       );
            }));
        }
    }
}