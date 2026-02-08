namespace Weblu.Application.Helpers
{
    public static class UrlValidator
    {
        // Validates if the provided string is a well-formed URL with HTTP or HTTPS scheme
        public static bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uri)
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }
    }
}