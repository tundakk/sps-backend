using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for testing rate limiting functionality
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RateLimitTestController : ControllerBase
    {
        private readonly ILogger<RateLimitTestController> _logger;

        /// <summary>
        /// Initializes a new instance of the RateLimitTestController
        /// </summary>
        /// <param name="logger">The logger instance</param>
        public RateLimitTestController(ILogger<RateLimitTestController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Test endpoint for general rate limiting
        /// </summary>
        [HttpGet("general")]
        public IActionResult TestGeneral()
        {
            _logger.LogInformation("General rate limit test endpoint called");
            return Ok(new 
            { 
                Message = "Request successful!", 
                Timestamp = DateTime.UtcNow,
                Endpoint = "General"
            });
        }

        /// <summary>
        /// Test endpoint for authenticated user rate limiting
        /// </summary>
        [HttpGet("authenticated")]
        [Authorize]
        public IActionResult TestAuthenticated()
        {
            var userId = User.Identity?.Name ?? "Unknown";
            _logger.LogInformation("Authenticated rate limit test endpoint called by user: {UserId}", userId);
            
            return Ok(new 
            { 
                Message = "Authenticated request successful!", 
                UserId = userId,
                Timestamp = DateTime.UtcNow,
                Endpoint = "Authenticated"
            });
        }

        /// <summary>
        /// Test endpoint that simulates heavy load
        /// </summary>
        [HttpPost("heavy")]
        public IActionResult TestHeavyOperation([FromBody] object? data = null)
        {
            _logger.LogInformation("Heavy operation endpoint called");
            
            // Simulate some processing time
            Thread.Sleep(100);
            
            return Ok(new 
            { 
                Message = "Heavy operation completed!", 
                ProcessingTime = "100ms",
                Timestamp = DateTime.UtcNow,
                Data = data
            });
        }

        /// <summary>
        /// Get current rate limit information for the client
        /// </summary>
        [HttpGet("info")]
        public IActionResult GetRateLimitInfo()
        {
            var headers = Response.Headers
                .Where(h => h.Key.StartsWith("X-RateLimit"))
                .ToDictionary(h => h.Key, h => h.Value.ToString());

            return Ok(new 
            { 
                Message = "Rate limit information",
                Headers = headers,
                ClientIP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Timestamp = DateTime.UtcNow
            });
        }
    }
}
