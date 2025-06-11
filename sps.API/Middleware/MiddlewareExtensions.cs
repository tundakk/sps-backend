using sps.Domain.Model.Models;

namespace sps.API.Middleware
{
    /// <summary>
    /// Extension methods for registering custom middleware
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Adds the JWT middleware to the application pipeline
        /// </summary>
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }

        /// <summary>
        /// Adds the global exception handler middleware to the application pipeline
        /// </summary>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }

        /// <summary>
        /// Adds the rate limiting middleware to the application pipeline
        /// </summary>
        /// <param name="builder">The application builder</param>
        /// <param name="rule">The rate limiting rule to apply</param>
        public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder builder, RateLimitRule rule)
        {
            return builder.UseMiddleware<RateLimitingMiddleware>(rule);
        }

        /// <summary>
        /// Adds the rate limiting middleware with default configuration
        /// </summary>
        /// <param name="builder">The application builder</param>
        public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder builder)
        {
            var defaultRule = new RateLimitRule
            {
                Limit = 100, // 100 requests
                Window = TimeSpan.FromMinutes(1) // per minute
            };
            
            return builder.UseRateLimiting(defaultRule);
        }
    }
}