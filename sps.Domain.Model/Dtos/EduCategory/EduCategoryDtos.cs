using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.Dtos.Education;
using sps.Domain.Model.Dtos.SpsaCase;

namespace sps.Domain.Model.Dtos.EduCategory
{
    /// <summary>
    /// Basic DTO for education category data
    /// </summary>
    public class EduCategoryDto
    {
        /// <summary>
        /// The unique identifier for the education category
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the education category
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Number of education programs in this category
        /// </summary>
        public int EducationCount { get; set; }
        
        /// <summary>
        /// Number of active SPSA cases in this category
        /// </summary>
        public int ActiveCaseCount { get; set; }
    }

    /// <summary>
    /// DTO for creating a new education category
    /// </summary>
    public class CreateEduCategoryDto
    {
        /// <summary>
        /// The name of the education category
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing education category
    /// </summary>
    public class UpdateEduCategoryDto
    {
        /// <summary>
        /// The unique identifier of the education category
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the education category
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }
    }

    /// <summary>
    /// Detailed DTO for education category including related data
    /// </summary>
    public class EduCategoryDetailDto
    {
        /// <summary>
        /// Basic category information
        /// </summary>
        public EduCategoryDto BasicInfo { get; set; }
        
        /// <summary>
        /// Education programs in this category
        /// </summary>
        public List<sps.Domain.Model.Dtos.Education.EducationDto> Educations { get; set; }
        
        /// <summary>
        /// SPSA cases in this category
        /// </summary>
        public List<SpsaCaseSummaryDto> Cases { get; set; }
        
        /// <summary>
        /// Statistics about support by period
        /// </summary>
        public List<PeriodSupportStats> SupportByPeriod { get; set; }
        
        public EduCategoryDetailDto()
        {
            Educations = new List<sps.Domain.Model.Dtos.Education.EducationDto>();
            Cases = new List<SpsaCaseSummaryDto>();
            SupportByPeriod = new List<PeriodSupportStats>();
        }
    }

    /// <summary>
    /// Support statistics for a specific period
    /// </summary>
    public class PeriodSupportStats
    {
        /// <summary>
        /// The period name
        /// </summary>
        public string PeriodName { get; set; }
        
        /// <summary>
        /// Number of active cases in this period
        /// </summary>
        public int ActiveCaseCount { get; set; }
        
        /// <summary>
        /// Total hours sought across all cases
        /// </summary>
        public int TotalHoursSought { get; set; }
        
        /// <summary>
        /// Total hours spent across all cases
        /// </summary>
        public int TotalHoursSpent { get; set; }
        
        /// <summary>
        /// Total amount paid for teacher support
        /// </summary>
        public decimal TeacherPayments { get; set; }
        
        /// <summary>
        /// Total amount paid for student support
        /// </summary>
        public decimal StudentPayments { get; set; }
    }
}