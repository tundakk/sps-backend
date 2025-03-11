// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using sps.Domain.Model.Dtos.SpsaCase;

// namespace sps.Domain.Model.Dtos.SupportType
// {
//     /// <summary>
//     /// Basic DTO for support type data
//     /// </summary>
//     public class SupportTypeDto
//     {
//         /// <summary>
//         /// The unique identifier for the support type
//         /// </summary>
//         public Guid Id { get; set; }
        
//         /// <summary>
//         /// The name of the support type
//         /// </summary>
//         public string Name { get; set; }
        
//         /// <summary>
//         /// Number of active cases using this support type
//         /// </summary>
//         public int ActiveCaseCount { get; set; }
        
//         /// <summary>
//         /// Total amount paid to teachers for this type of support
//         /// </summary>
//         public decimal TotalTeacherPayments { get; set; }
        
//         /// <summary>
//         /// Total amount paid to students for this type of support
//         /// </summary>
//         public decimal TotalStudentPayments { get; set; }
//     }

//     /// <summary>
//     /// DTO for creating a new support type
//     /// </summary>
//     public class CreateSupportTypeDto
//     {
//         /// <summary>
//         /// The name of the support type
//         /// </summary>
//         [Required]
//         [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
//         public string Name { get; set; }
//     }

//     /// <summary>
//     /// DTO for updating an existing support type
//     /// </summary>
//     public class UpdateSupportTypeDto
//     {
//         /// <summary>
//         /// The unique identifier of the support type
//         /// </summary>
//         [Required]
//         public Guid Id { get; set; }
        
//         /// <summary>
//         /// The name of the support type
//         /// </summary>
//         [Required]
//         [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
//         public string Name { get; set; }
//     }

//     /// <summary>
//     /// Detailed DTO for support type including related data
//     /// </summary>
//     public class SupportTypeDetailDto
//     {
//         /// <summary>
//         /// Basic support type information
//         /// </summary>
//         public SupportTypeDto BasicInfo { get; set; }
        
//         /// <summary>
//         /// Cases using this type of support
//         /// </summary>
//         public List<SpsaCaseSummaryDto> Cases { get; set; }
        
//         /// <summary>
//         /// Payment statistics by period
//         /// </summary>
//         public List<PeriodPaymentStats> PaymentsByPeriod { get; set; }
        
//         public SupportTypeDetailDto()
//         {
//             Cases = new List<SpsaCaseSummaryDto>();
//             PaymentsByPeriod = new List<PeriodPaymentStats>();
//         }
//     }

//     /// <summary>
//     /// Payment statistics for a specific period
//     /// </summary>
//     public class PeriodPaymentStats
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
//         /// Number of cases in this period
//         /// </summary>
//         public int CaseCount { get; set; }
        
//         /// <summary>
//         /// Total amount paid to teachers in this period
//         /// </summary>
//         public decimal TeacherPayments { get; set; }
        
//         /// <summary>
//         /// Total amount paid to students in this period
//         /// </summary>
//         public decimal StudentPayments { get; set; }
//     }
// }