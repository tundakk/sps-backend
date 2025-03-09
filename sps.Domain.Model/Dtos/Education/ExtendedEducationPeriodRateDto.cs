using System;

namespace sps.Domain.Model.Dtos.Education
{
    /// <summary>
    /// Extended DTO for education period rate information with dates
    /// </summary>
    public class ExtendedEducationPeriodRateDto
    {
        /// <summary>
        /// The unique identifier for the period rate
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The period this rate applies to
        /// </summary>
        public string PeriodName { get; set; }
        
        /// <summary>
        /// Start date of the period
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// End date of the period
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// The rate amount for this period
        /// </summary>
        public decimal Rate { get; set; }
    }
}