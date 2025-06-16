using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.Domain.Model.Responses;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Simple controller to demonstrate middleware-based rate limiting.
    /// Shows how the middleware automatically applies rate limits to all endpoints.
    /// </summary>
    [Route("api/[controller]")]
    public class RateLimitBasicController : BaseController<RateLimitBasicController>
    {
        /// <summary>
        /// Initializes a new instance of the RateLimitBasicController class.
        /// </summary>
        /// <param name="logger">The logger instance</param>
        public RateLimitBasicController(ILogger<RateLimitBasicController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Basic endpoint that uses middleware rate limiting only.
        /// Should be limited by the general API rate limit (500 requests/minute).
        /// </summary>
        /// <returns>Basic response with rate limiting info</returns>
        [HttpGet("test")]
        public IActionResult Test()
        {
            Logger.LogInformation("Basic rate limit test endpoint accessed at {Time}", DateTime.UtcNow);

            var response = ServiceResponse<object>.CreateSuccess(
                new
                {
                    message = "Middleware rate limiting is working!",
                    timestamp = DateTime.UtcNow,
                    rateLimitingInfo = new
                    {
                        appliedBy = "Rate Limiting Middleware",
                        generalLimit = "500 requests per minute",
                        checkHeaders = "Look for X-RateLimit-* headers in response"
                    }
                });

            return Ok(response);
        }

        /// <summary>
        /// Another endpoint to test rate limiting across multiple endpoints.
        /// </summary>
        /// <returns>Info about rate limiting</returns>
        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            var rateLimitHeaders = new Dictionary<string, string>();

            // Note: Headers are added by middleware after this action completes
            var response = ServiceResponse<object>.CreateSuccess(
                new
                {
                    message = "Rate limiting middleware information",
                    activeMiddleware = "RateLimitingMiddleware",
                    configuration = new
                    {
                        generalApiLimit = "500 requests per minute",
                        authEndpointsLimit = "50 requests per 5 minutes",
                        slidingWindow = "Yes - timestamps expire automatically"
                    },
                    instructions = new
                    {
                        checkHeaders = "Response includes X-RateLimit-Limit, X-RateLimit-Remaining, X-RateLimit-Window",
                        testRateLimit = "Make multiple rapid requests to see remaining count decrease",
                        exceeedLimit = "After limit is reached, you'll get 429 Too Many Requests"
                    },
                    timestamp = DateTime.UtcNow
                });

            return Ok(response);
        }

        /// <summary>
        /// POST endpoint to test rate limiting on different HTTP methods.
        /// </summary>
        /// <param name="data">Some test data</param>
        /// <returns>Response showing rate limiting is applied</returns>
        [HttpPost("submit")]
        public IActionResult Submit([FromBody] TestData? data)
        {
            Logger.LogInformation("POST endpoint accessed with data: {Data}", data?.Message ?? "null");
            var response = ServiceResponse<object>.CreateSuccess(
              new
              {
                  message = "POST request processed with rate limiting",
                  receivedData = data?.Message ?? "No data provided",
                  rateLimiting = "Applied by middleware to all HTTP methods",
                  timestamp = DateTime.UtcNow
              });

            return Ok(response);
        }
    }    /// <summary>
         /// Test data model for basic rate limiting endpoints
         /// </summary>
    public class TestData
    {
        /// <summary>
        /// Gets or sets the message
        /// </summary>
        public string? Message { get; set; }
    }
}
