using System;
using System.Collections.Generic;
using sps.Domain.Model.Dtos.Student;

namespace sps.Domain.Model.Dtos.Period
{
    /// <summary>
    /// Detailed DTO for Period data including related collections
    /// </summary>
    public class PeriodDetailDto
    {
        /// <summary>
        /// Basic period information
        /// </summary>
        public PeriodDto BasicInfo { get; set; }
        
        /// <summary>
        /// Education rates for this period
        /// </summary>
        public List<PeriodRateInfo> EducationRates { get; set; }
        
        /// <summary>
        /// Students who started in this period
        /// </summary>
        public List<StudentSummaryDto> Students { get; set; }
        
        /// <summary>
        /// SPSA cases associated with this period
        /// </summary>
        public List<PeriodCaseInfo> Cases { get; set; }
        
        public PeriodDetailDto()
        {
            EducationRates = new List<PeriodRateInfo>();
            Students = new List<StudentSummaryDto>();
            Cases = new List<PeriodCaseInfo>();
        }
    }
}