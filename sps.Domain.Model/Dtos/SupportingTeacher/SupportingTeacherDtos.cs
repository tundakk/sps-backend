// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using sps.Domain.Model.Dtos.SpsaCase;

// namespace sps.Domain.Model.Dtos.SupportingTeacher
// {
//     /// <summary>
//     /// Basic DTO for supporting teacher data
//     /// </summary>
//     public class SupportingTeacherDto
//     {
//         /// <summary>
//         /// The unique identifier for the teacher
//         /// </summary>
//         public Guid Id { get; set; }
        
//         /// <summary>
//         /// The name of the teacher
//         /// </summary>
//         public string Name { get; set; }
        
//         /// <summary>
//         /// The teacher's email address
//         /// </summary>
//         public string Email { get; set; }
        
//         /// <summary>
//         /// The name of the place/department
//         /// </summary>
//         public string PlaceName { get; set; }
        
//         /// <summary>
//         /// Number of active cases assigned to this teacher
//         /// </summary>
//         public int ActiveCaseCount { get; set; }
//     }

//     /// <summary>
//     /// DTO for creating a new supporting teacher
//     /// </summary>
//     public class CreateSupportingTeacherDto
//     {
//         /// <summary>
//         /// The name of the teacher
//         /// </summary>
//         [Required]
//         [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
//         public string Name { get; set; }
        
//         /// <summary>
//         /// The teacher's email address
//         /// </summary>
//         [Required]
//         [EmailAddress]
//         public string Email { get; set; }
        
//         /// <summary>
//         /// The ID of the place/department
//         /// </summary>
//         public Guid? PlacesId { get; set; }
//     }

//     /// <summary>
//     /// DTO for updating an existing supporting teacher
//     /// </summary>
//     public class UpdateSupportingTeacherDto
//     {
//         /// <summary>
//         /// The unique identifier of the teacher
//         /// </summary>
//         [Required]
//         public Guid Id { get; set; }
        
//         /// <summary>
//         /// The name of the teacher
//         /// </summary>
//         [Required]
//         [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
//         public string Name { get; set; }
        
//         /// <summary>
//         /// The teacher's email address
//         /// </summary>
//         [Required]
//         [EmailAddress]
//         public string Email { get; set; }
        
//         /// <summary>
//         /// The ID of the place/department
//         /// </summary>
//         public Guid? PlacesId { get; set; }
//     }

//     /// <summary>
//     /// Detailed DTO for supporting teacher including related data
//     /// </summary>
//     public class SupportingTeacherDetailDto
//     {
//         /// <summary>
//         /// Basic teacher information
//         /// </summary>
//         public SupportingTeacherDto BasicInfo { get; set; }
        
//         /// <summary>
//         /// Cases assigned to this teacher
//         /// </summary>
//         public List<SpsaCaseSummaryDto> Cases { get; set; }
        
//         /// <summary>
//         /// Workload statistics by period
//         /// </summary>
//         public List<PeriodWorkload> WorkloadByPeriod { get; set; }
        
//         public SupportingTeacherDetailDto()
//         {
//             Cases = new List<SpsaCaseSummaryDto>();
//             WorkloadByPeriod = new List<PeriodWorkload>();
//         }
//     }

//     /// <summary>
//     /// Workload statistics for a specific period
//     /// </summary>
//     public class PeriodWorkload
//     {
//         /// <summary>
//         /// The name of the period
//         /// </summary>
//         public string PeriodName { get; set; }
        
//         /// <summary>
//         /// Start date of the period
//         /// </summary>
//         public DateTime StartDate { get; set; }
        
//         /// <summary>
//         /// End date of the period
//         /// </summary>
//         public DateTime EndDate { get; set; }
        
//         /// <summary>
//         /// Number of active cases in this period
//         /// </summary>
//         public int ActiveCaseCount { get; set; }
        
//         /// <summary>
//         /// Total hours sought across all cases
//         /// </summary>
//         public int TotalHoursSought { get; set; }
        
//         /// <summary>
//         /// Total hours spent on cases
//         /// </summary>
//         public int TotalHoursSpent { get; set; }
        
//         /// <summary>
//         /// Total amount paid for support
//         /// </summary>
//         public decimal TotalPayment { get; set; }
//     }
// }