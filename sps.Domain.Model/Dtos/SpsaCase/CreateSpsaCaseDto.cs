using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos.SpsaCase
{
    /// <summary>
    /// DTO for creating a new SPSA case
    /// </summary>
    public class CreateSpsaCaseDto
    {
        /// <summary>
        /// The case reference number
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Case number must be between 1 and 50 characters")]
        public string SpsaCaseNumber { get; set; }
        
        /// <summary>
        /// Number of hours requested for support
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Hours sought must be greater than 0")]
        public int HoursSought { get; set; }
        
        /// <summary>
        /// Optional comments about the case
        /// </summary>
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
        public string Comment { get; set; }
        
        /// <summary>
        /// Additional comments about the case
        /// </summary>
        public List<string> Comments { get; set; } = new List<string>();
        
        /// <summary>
        /// The ID of the student this case belongs to
        /// </summary>
        [Required]
        public Guid StudentId { get; set; }
        
        /// <summary>
        /// The ID of the supporting teacher (if assigned)
        /// </summary>
        public Guid? SupportingTeacherId { get; set; }
        
        /// <summary>
        /// The ID of the period when the application was made
        /// </summary>
        public Guid? AppliedPeriodId { get; set; }
        
        /// <summary>
        /// The ID of the diagnosis (if applicable)
        /// </summary>
        public Guid? DiagnosisId { get; set; }
        
        /// <summary>
        /// The ID of the education category
        /// </summary>
        public Guid? EduCategoryId { get; set; }
        
        /// <summary>
        /// The ID of the support type
        /// </summary>
        public Guid? SupportTypeId { get; set; }
        
        /// <summary>
        /// The ID of the education status
        /// </summary>
        public Guid? EduStatusId { get; set; }
        
        /// <summary>
        /// The date when the application was submitted
        /// </summary>
        public DateTime? ApplicationDate { get; set; }
        
        /// <summary>
        /// Whether course description was received
        /// </summary>
        public bool CourseDescriptionReceived { get; set; }
        
        /// <summary>
        /// Whether timesheet was received
        /// </summary>
        public bool TimesheetReceived { get; set; }
    }
}