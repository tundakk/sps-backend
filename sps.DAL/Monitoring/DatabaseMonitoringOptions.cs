namespace sps.DAL.Monitoring
{
    /// <summary>
    /// Configuration options for database performance monitoring
    /// </summary>
    public class DatabaseMonitoringOptions
    {
        /// <summary>
        /// Gets or sets whether database profiling is enabled
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the number of days to retain profiling data
        /// </summary>
        public int RetentionDays { get; set; } = 7;

        /// <summary>
        /// Gets or sets whether server timing headers should be included in responses
        /// </summary>
        public bool IncludeServerTimingHeader { get; set; } = true;

        /// <summary>
        /// Gets or sets the path for accessing the MiniProfiler UI
        /// </summary>
        public string RouteBasePath { get; set; } = "/profiler";

        /// <summary>
        /// Gets or sets whether to track connection open/close events
        /// </summary>
        public bool TrackConnectionOpenClose { get; set; } = true;

        /// <summary>
        /// Gets or sets whether profiling is restricted to admin users only
        /// </summary>
        public bool RestrictToAdminOnly { get; set; } = true;
    }
}
