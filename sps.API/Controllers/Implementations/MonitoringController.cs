using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Profiling;
using System.Text;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for database performance monitoring features
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MonitoringController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringController"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration instance.</param>
        public MonitoringController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets a summary of the latest database performance statistics
        /// </summary>
        /// <returns>Summary of recent database operations and performance metrics</returns>
        [HttpGet("database/summary")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDatabasePerformanceSummary()
        {
            var profiler = MiniProfiler.Current;
            if (profiler == null)
            {
                return Ok(new { message = "Profiling is not currently active for this request" });
            }
            return Ok(new
            {
                requestId = profiler.Id,
                name = profiler.Name,
                started = profiler.Started,
                durationMilliseconds = profiler.DurationMilliseconds,
                rootTimingId = profiler.Root?.Id,
                hasRootTiming = profiler.Root != null,
                profilerUrl = $"/profiler/results?id={profiler.Id}"
            });
        }

        /// <summary>
        /// Gets the URL to the MiniProfiler dashboard
        /// </summary>
        /// <returns>URL to the MiniProfiler dashboard</returns>
        [HttpGet("database/dashboard")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDashboardUrl()
        {
            return Ok(new { url = "/profiler/results-index" });
        }
    }
}
