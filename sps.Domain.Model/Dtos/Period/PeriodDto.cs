using System;

namespace sps.Domain.Model.Dtos.Period
{
    /// <summary>
    /// DTO representing a time period in the system
    /// </summary>
    public class PeriodDto
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the period name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the start date of the period
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the period
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the period is currently active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the number of students in this period
        /// </summary>
        public int StudentCount { get; set; }

        /// <summary>
        /// Gets or sets the number of active cases in this period
        /// </summary>
        public int ActiveCaseCount { get; set; }
    }
}