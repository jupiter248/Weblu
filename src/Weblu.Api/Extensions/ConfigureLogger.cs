using Serilog;

namespace Weblu.Api.Extensions
{
    public static class ConfigureLogger
    {
        public static void ApplySerilog(this ConfigureHostBuilder hostBuilder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341") // Seq running in Docker
                .CreateLogger();

            hostBuilder.UseSerilog();
        }
    }
}