using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using sps.API.Configuration;
using sps.Domain.Model.Models;
using System.Net;

namespace sps.API.Attributes
{
    /// <summary>
    /// Attribute for applying rate limiting to controller actions or entire controllers.
    /// Can override default rate limiting configured in BaseController.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class RateLimitAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Maximum number of requests allowed within the time window.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Time window in minutes for the rate limit.
        /// </summary>
        public int WindowMinutes { get; set; }

        /// <summary>
        /// Whether to include user ID in the rate limiting key (for authenticated requests).
        /// </summary>
        public bool IncludeUserId { get; set; } = false;

        /// <summary>
        /// Optional key prefix for this specific rate limit rule.
        /// </summary>
        public string? KeyPrefix { get; set; }

        /// <summary>
        /// Initializes a new instance of the RateLimitAttribute with specified parameters.
        /// </summary>
        /// <param name="limit">Maximum number of requests allowed</param>
        /// <param name="windowMinutes">Time window in minutes</param>
        public RateLimitAttribute(int limit, int windowMinutes)
        {
            Limit = limit;
            WindowMinutes = windowMinutes;
        }

        /// <summary>
        /// Executes before the action method is invoked.
        /// </summary>
        /// <param name="context">The action executing context</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var memoryCache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<RateLimitAttribute>>();            // Create a rate limit rule from the attribute values
            var rule = new RateLimitRule
            {
                Limit = Limit,
                Window = TimeSpan.FromMinutes(WindowMinutes),
                IncludeUserId = IncludeUserId,
                EndpointPattern = KeyPrefix ?? $"{context.Controller.GetType().Name}.{context.ActionDescriptor.DisplayName}"
            };

            // Build the cache key
            var clientIp = GetClientIpAddress(context.HttpContext);
            var userId = IncludeUserId ? GetUserId(context.HttpContext) : null;
            var cacheKey = BuildCacheKey(rule, clientIp, userId);

            logger.LogDebug("Checking rate limit for key: {CacheKey}", cacheKey);

            // Check and update rate limit
            if (!CheckRateLimit(memoryCache, cacheKey, rule, logger))
            {                // Rate limit exceeded
                logger.LogWarning("Rate limit exceeded for {CacheKey}", cacheKey);

                context.HttpContext.Response.Headers["X-RateLimit-Limit"] = Limit.ToString();
                context.HttpContext.Response.Headers["X-RateLimit-Remaining"] = "0";
                context.HttpContext.Response.Headers["X-RateLimit-Window"] = (WindowMinutes * 60).ToString();

                context.Result = new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.TooManyRequests,
                    Content = "Rate limit exceeded. Too many requests.",
                    ContentType = "text/plain"
                };
                return;
            }            // Add rate limit headers
            var remaining = GetRemainingRequests(memoryCache, cacheKey, rule);
            context.HttpContext.Response.Headers["X-RateLimit-Limit"] = Limit.ToString();
            context.HttpContext.Response.Headers["X-RateLimit-Remaining"] = remaining.ToString();
            context.HttpContext.Response.Headers["X-RateLimit-Window"] = (WindowMinutes * 60).ToString();

            base.OnActionExecuting(context);
        }        private bool CheckRateLimit(IMemoryCache cache, string key, RateLimitRule rule, ILogger logger)
        {
            var now = DateTime.UtcNow;
            var windowStart = now.Subtract(rule.Window);

            if (cache.TryGetValue(key, out List<DateTime>? timestamps))
            {
                // Remove old timestamps outside the window
                timestamps = timestamps!.Where(t => t > windowStart).ToList();

                if (timestamps.Count >= rule.Limit)
                {
                    logger.LogDebug("Rate limit check failed: {Count}/{Limit} requests in window", 
                        timestamps.Count, rule.Limit);
                    return false;
                }

                // Add current timestamp
                timestamps.Add(now);
            }
            else
            {
                timestamps = new List<DateTime> { now };
            }            // Update cache with sliding expiration
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = rule.Window,
                SlidingExpiration = TimeSpan.FromTicks(rule.Window.Ticks / 2)
            };

            cache.Set(key, timestamps, cacheEntryOptions);
            
            logger.LogDebug("Rate limit check passed: {Count}/{Limit} requests in window", 
                timestamps.Count, rule.Limit);
            
            return true;
        }        private int GetRemainingRequests(IMemoryCache cache, string key, RateLimitRule rule)
        {
            if (cache.TryGetValue(key, out List<DateTime>? timestamps))
            {
                var windowStart = DateTime.UtcNow.Subtract(rule.Window);
                var validTimestamps = timestamps!.Where(t => t > windowStart).Count();
                return Math.Max(0, rule.Limit - validTimestamps);
            }

            return rule.Limit;
        }

        private string BuildCacheKey(RateLimitRule rule, string clientIp, string? userId)
        {
            var keyParts = new List<string> { "rate_limit", rule.EndpointPattern ?? "default", clientIp };
            
            if (rule.IncludeUserId && !string.IsNullOrEmpty(userId))
            {
                keyParts.Add(userId);
            }

            return string.Join(":", keyParts);
        }

        private string GetClientIpAddress(HttpContext context)
        {
            // Try to get IP from various headers (for load balancers/proxies)
            var ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',').FirstOrDefault()?.Trim()
                         ?? context.Request.Headers["X-Real-IP"].FirstOrDefault()
                         ?? context.Request.Headers["CF-Connecting-IP"].FirstOrDefault()
                         ?? context.Connection.RemoteIpAddress?.ToString()
                         ?? "unknown";

            return ipAddress;
        }

        private string? GetUserId(HttpContext context)
        {
            return context.User?.Identity?.Name 
                ?? context.User?.FindFirst("sub")?.Value 
                ?? context.User?.FindFirst("id")?.Value;
        }
    }
}
