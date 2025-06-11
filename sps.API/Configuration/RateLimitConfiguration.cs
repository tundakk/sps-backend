using sps.Domain.Model.Models;

namespace sps.API.Configuration
{
    /// <summary>
    /// Configuration class for rate limiting settings
    /// </summary>
    public class RateLimitConfiguration
    {
        /// <summary>
        /// Configuration section name in appsettings.json
        /// </summary>
        public const string SectionName = "RateLimit";

        /// <summary>
        /// General rate limiting rule for all endpoints
        /// </summary>
        public RateLimitConfigSection? General { get; set; }

        /// <summary>
        /// Specific rate limiting rule for API endpoints
        /// </summary>
        public RateLimitConfigSection? Api { get; set; }

        /// <summary>
        /// Specific rate limiting rule for authentication endpoints
        /// </summary>
        public RateLimitConfigSection? Auth { get; set; }

        /// <summary>
        /// Gets all configured rate limit rules
        /// </summary>
        public IEnumerable<RateLimitRule> GetRules()
        {
            var rules = new List<RateLimitRule>();

            if (Auth != null)
            {
                rules.Add(Auth.ToRateLimitRule());
            }

            if (Api != null)
            {
                rules.Add(Api.ToRateLimitRule());
            }

            if (General != null)
            {
                rules.Add(General.ToRateLimitRule());
            }

            return rules;
        }
    }

    /// <summary>
    /// Configuration section for a single rate limit rule
    /// </summary>
    public class RateLimitConfigSection
    {
        /// <summary>
        /// Maximum number of requests allowed within the time window
        /// </summary>
        public int Limit { get; set; } = 100;

        /// <summary>
        /// Time window in minutes
        /// </summary>
        public int WindowMinutes { get; set; } = 1;

        /// <summary>
        /// Optional endpoint pattern to apply the rule to
        /// </summary>
        public string? EndpointPattern { get; set; }

        /// <summary>
        /// Whether to include the user ID in the rate limit key
        /// </summary>
        public bool IncludeUserId { get; set; } = false;

        /// <summary>
        /// Converts this configuration section to a RateLimitRule
        /// </summary>
        public RateLimitRule ToRateLimitRule()
        {
            return new RateLimitRule
            {
                Limit = Limit,
                Window = TimeSpan.FromMinutes(WindowMinutes),
                EndpointPattern = EndpointPattern,
                IncludeUserId = IncludeUserId
            };
        }
    }
}
