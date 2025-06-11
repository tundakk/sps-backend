using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using sps.Domain.Model.Models;
using System.Net;
using System.Text.Json;
using System.Security.Claims;

namespace sps.API.Middleware
{
    /// <summary>
    /// Middleware for implementing rate limiting functionality
    /// </summary>
    public class RateLimitingMiddleware
    {        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly RateLimitRule _rule;
        private readonly ILogger<RateLimitingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the RateLimitingMiddleware
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        /// <param name="cache">Memory cache for storing rate limit data</param>
        /// <param name="rule">Rate limiting rule configuration</param>
        /// <param name="logger">Logger instance</param>
        public RateLimitingMiddleware(
            RequestDelegate next, 
            IMemoryCache cache, 
            RateLimitRule rule, 
            ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _cache = cache;
            _rule = rule;
            _logger = logger;
        }

        /// <summary>
        /// Processes the HTTP request and applies rate limiting
        /// </summary>
        /// <param name="context">The HTTP context</param>
        public async Task InvokeAsync(HttpContext context)
        {
            // Skip rate limiting for certain paths if needed (like health checks)
            if (ShouldSkipRateLimit(context))
            {
                await _next(context);
                return;
            }

            // Check if this endpoint matches the pattern (if specified)
            if (!string.IsNullOrEmpty(_rule.EndpointPattern) && 
                !context.Request.Path.StartsWithSegments(_rule.EndpointPattern))
            {
                await _next(context);
                return;
            }

            var identifier = GetClientIdentifier(context);
            var cacheKey = $"RateLimit_{identifier}";

            if (!_cache.TryGetValue(cacheKey, out int requestCount))
            {
                requestCount = 0;
                _cache.Set(cacheKey, requestCount, _rule.Window);
            }

            if (requestCount >= _rule.Limit)
            {
                _logger.LogWarning("Rate limit exceeded for client: {Identifier}. Count: {Count}, Limit: {Limit}", 
                    identifier, requestCount, _rule.Limit);

                await HandleRateLimitExceeded(context);
                return;
            }

            // Increment the request count
            _cache.Set(cacheKey, ++requestCount, _rule.Window);

            // Add rate limit headers to the response
            AddRateLimitHeaders(context, requestCount);

            await _next(context);
        }

        /// <summary>
        /// Determines if rate limiting should be skipped for this request
        /// </summary>
        private static bool ShouldSkipRateLimit(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLowerInvariant();
            
            // Skip rate limiting for health checks, swagger, and other system endpoints
            return path != null && (
                path.StartsWith("/health") ||
                path.StartsWith("/swagger") ||
                path.StartsWith("/api/docs") ||
                path.StartsWith("/.well-known")
            );
        }

        /// <summary>
        /// Gets a unique identifier for the client (IP address or user ID)
        /// </summary>
        private string GetClientIdentifier(HttpContext context)
        {
            var identifier = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            // If the rule includes user ID and the user is authenticated, include user ID
            if (_rule.IncludeUserId && context.User.Identity?.IsAuthenticated == true)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    identifier = $"{identifier}_{userId}";
                }
            }

            return identifier;
        }

        /// <summary>
        /// Handles the response when rate limit is exceeded
        /// </summary>
        private static async Task HandleRateLimitExceeded(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = "Rate limit exceeded. Please try again later.",
                StatusCode = 429,
                Timestamp = DateTime.UtcNow
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }

        /// <summary>
        /// Adds rate limit information to response headers
        /// </summary>
        private void AddRateLimitHeaders(HttpContext context, int currentCount)
        {
            context.Response.Headers.TryAdd("X-RateLimit-Limit", _rule.Limit.ToString());
            context.Response.Headers.TryAdd("X-RateLimit-Remaining", Math.Max(0, _rule.Limit - currentCount).ToString());
            context.Response.Headers.TryAdd("X-RateLimit-Window", _rule.Window.TotalSeconds.ToString());
        }
    }
}
