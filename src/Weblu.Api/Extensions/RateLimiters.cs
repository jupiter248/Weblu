using System.Threading.RateLimiting;

namespace Weblu.Api.Extensions
{
    public static class RateLimiters
    {
        public static void ApplyGlobalRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                {
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: ip,
                        factory: key => new FixedWindowRateLimiterOptions()
                        {
                            PermitLimit = 200,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        }
                    );
                });
            });
        }
        public static void ApplyAuthRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddPolicy("AuthPolicy", context =>
                {
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: ip,
                        factory: key => new FixedWindowRateLimiterOptions()
                        {
                            PermitLimit = 4,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        }
                    );
                });
            });
        }
        public static void ApplyViewArticleRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddPolicy("ArticleViewPolicy", context =>
                {
                    var route = context.GetRouteData();
                    var articleId = route?.Values["id"]?.ToString() ?? "unknown";

                    return RateLimitPartition.GetTokenBucketLimiter(
                        partitionKey: $"{context.Connection.RemoteIpAddress}-{articleId}",
                    factory: key => new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = 3,                 // max number of views
                        TokensPerPeriod = 5,            // refill amount
                        ReplenishmentPeriod = TimeSpan.FromMinutes(1),
                        AutoReplenishment = true,
                        QueueLimit = 0
                    });
                });
            });
        }
    }
}