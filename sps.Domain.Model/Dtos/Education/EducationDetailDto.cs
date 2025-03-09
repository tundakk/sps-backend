using System;
using System.Collections.Generic;
using sps.Domain.Model.Dtos.Student;

namespace sps.Domain.Model.Dtos.Education
{
    /// <summary>
    /// Detailed DTO for education program including related data
    /// </summary>
    public class EducationDetailDto
    {
        /// <summary>
        /// Basic program information
        /// </summary>
        public EducationDto BasicInfo { get; set; }
        
        /// <summary>
        /// The category this program belongs to
        /// </summary>
        public EducationCategoryDto Category { get; set; }
        
        /// <summary>
        /// Rate configurations by period
        /// </summary>
        public List<ExtendedEducationPeriodRateDto> PeriodRates { get; set; }
        
        /// <summary>
        /// Students enrolled in this program
        /// </summary>
        public List<StudentSummaryDto> Students { get; set; }
        
        public EducationDetailDto()
        {
            PeriodRates = new List<ExtendedEducationPeriodRateDto>();
            Students = new List<StudentSummaryDto>();
        }
    }
}