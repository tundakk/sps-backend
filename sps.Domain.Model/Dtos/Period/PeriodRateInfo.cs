using System;

namespace sps.Domain.Model.Dtos.Period
{
    /// <summary>
    /// Education rate information for a specific period
    /// </summary>
    public class PeriodRateInfo
    {
        /// <summary>
        /// The unique identifier of the rate entry
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the education program
        /// </summary>
        public string EducationName { get; set; }
        
        /// <summary>
        /// The education category
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// The rate amount for this period
        /// </summary>
        public decimal Rate { get; set; }
    }
}