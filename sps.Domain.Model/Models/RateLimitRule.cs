namespace sps.Domain.Model.Models
{
    /// <summary>
    /// Represents a rate limiting rule configuration
    /// </summary>
    public class RateLimitRule
    {
        /// <summary>
        /// Maximum number of requests allowed within the specified time window
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Time window for the rate limit
        /// </summary>
        public TimeSpan Window { get; set; }

        /// <summary>
        /// Optional endpoint pattern to apply the rule to (if null, applies to all endpoints)
        /// </summary>
        public string? EndpointPattern { get; set; }

        /// <summary>
        /// Whether to include the user ID in the rate limit key (for authenticated users)
        /// </summary>
        public bool IncludeUserId { get; set; } = false;
    }
}
