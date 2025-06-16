using Microsoft.AspNetCore.Mvc;
using sps.API.Attributes;
using sps.API.Controllers.Base;
using sps.Domain.Model.Responses;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Demo controller showcasing different rate limiting configurations.
    /// Demonstrates how to override default rate limits using attributes.
    /// </summary>
    [Route("api/[controller]")]
    [RateLimit(50, 1)] // Controller-level: 50 requests per minute
    public class RateLimitDemoController : BaseController<RateLimitDemoController>
    {
        /// <summary>
        /// Initializes a new instance of the RateLimitDemoController class.
        /// </summary>
        /// <param name="logger">The logger instance</param>
        public RateLimitDemoController(ILogger<RateLimitDemoController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Standard endpoint using controller-level rate limiting.
        /// Limited to 50 requests per minute.
        /// </summary>
        /// <returns>Standard response</returns>
        [HttpGet("standard")]
        public IActionResult StandardEndpoint()
        {
            Logger.LogInformation("Standard endpoint accessed at {Time}", DateTime.UtcNow);
            var response = ServiceResponse<object>.CreateSuccess(
              new { message = "Standard endpoint - 50 requests per minute", timestamp = DateTime.UtcNow });

            return Ok(response);
        }

        /// <summary>
        /// High-frequency endpoint with relaxed rate limiting.
        /// Limited to 200 requests per minute.
        /// </summary>
        /// <returns>High frequency response</returns>
        [HttpGet("high-frequency")]
        [RateLimit(200, 1)] // Override: 200 requests per minute
        public IActionResult HighFrequencyEndpoint()
        {
            Logger.LogInformation("High-frequency endpoint accessed at {Time}", DateTime.UtcNow);
            var response = ServiceResponse<object>.CreateSuccess(
              new { message = "High-frequency endpoint - 200 requests per minute", timestamp = DateTime.UtcNow });

            return Ok(response);
        }

        /// <summary>
        /// Sensitive endpoint with strict rate limiting.
        /// Limited to 5 requests per 5 minutes to prevent abuse.
        /// </summary>
        /// <returns>Sensitive operation response</returns>
        [HttpPost("sensitive")]
        [RateLimit(5, 5)] // Override: 5 requests per 5 minutes
        public IActionResult SensitiveEndpoint([FromBody] SensitiveRequest request)
        {
            Logger.LogWarning("Sensitive endpoint accessed at {Time} with data: {Data}",
                DateTime.UtcNow, request?.Data ?? "null");

            var response = ServiceResponse<object>.CreateSuccess(
                new
                {
                    message = "Sensitive endpoint - 5 requests per 5 minutes",
                    timestamp = DateTime.UtcNow,
                    processedData = request?.Data ?? "No data provided"
                });

            return Ok(response);
        }

        /// <summary>
        /// Bulk operation endpoint with hourly rate limiting.
        /// Limited to 10 requests per hour for resource-intensive operations.
        /// </summary>
        /// <returns>Bulk operation response</returns>
        [HttpPost("bulk-operation")]
        [RateLimit(10, 60)] // Override: 10 requests per hour
        public async Task<IActionResult> BulkOperationEndpoint([FromBody] BulkRequest request)
        {
            Logger.LogInformation("Bulk operation started at {Time} for {Count} items",
                DateTime.UtcNow, request?.Items?.Count ?? 0);

            // Simulate processing time
            await Task.Delay(1000);
            var response = ServiceResponse<object>.CreateSuccess(
              new
              {
                  message = "Bulk operation endpoint - 10 requests per hour",
                  timestamp = DateTime.UtcNow,
                  itemsProcessed = request?.Items?.Count ?? 0,
                  processingTimeMs = 1000
              });

            return Ok(response);
        }

        /// <summary>
        /// Status endpoint with no additional rate limiting.
        /// Uses controller-level rate limiting (50 requests per minute).
        /// </summary>
        /// <returns>Status information</returns>
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            var response = ServiceResponse<object>.CreateSuccess(
                new
                {
                    status = "healthy",
                    timestamp = DateTime.UtcNow,
                    rateLimitInfo = new
                    {
                        controllerDefault = "50 requests per minute",
                        middleware = "Also active for general API protection"
                    }
                });

            return Ok(response);
        }

        /// <summary>
        /// Get rate limiting information for this controller.
        /// </summary>
        /// <returns>Rate limiting configuration details</returns>
        [HttpGet("rate-limit-info")]
        public IActionResult GetRateLimitInfo()
        {
            var endpoints = new[]
            {
                new { endpoint = "GET /standard", limit = "50 requests per minute (controller default)" },
                new { endpoint = "GET /high-frequency", limit = "200 requests per minute (override)" },
                new { endpoint = "POST /sensitive", limit = "5 requests per 5 minutes (override)" },
                new { endpoint = "POST /bulk-operation", limit = "10 requests per hour (override)" },
                new { endpoint = "GET /status", limit = "50 requests per minute (controller default)" },
                new { endpoint = "GET /rate-limit-info", limit = "50 requests per minute (controller default)" }
            }; var response = ServiceResponse<object>.CreateSuccess(
                new
                {
                    controllerName = "RateLimitDemoController",
                    defaultRateLimit = "50 requests per minute",
                    endpoints = endpoints,
                    middleware = "Rate limiting middleware also active for general API protection",
                    timestamp = DateTime.UtcNow
                });

            return Ok(response);
        }
    }

    #region Request Models

    /// <summary>
    /// Request model for sensitive operations
    /// </summary>
    public class SensitiveRequest
    {
        /// <summary>
        /// Gets or sets the sensitive data
        /// </summary>
        public string? Data { get; set; }
    }

    /// <summary>
    /// Request model for bulk operations
    /// </summary>
    public class BulkRequest
    {
        /// <summary>
        /// Gets or sets the list of items to process
        /// </summary>
        public List<BulkItem>? Items { get; set; }
    }

    /// <summary>
    /// Individual item in a bulk request
    /// </summary>
    public class BulkItem
    {
        /// <summary>
        /// Gets or sets the item identifier
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Gets or sets the item name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Gets or sets the item data
        /// </summary>
        public string? Data { get; set; }
    }

    #endregion
}
