// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using sps.Domain.Model.Dtos.SpsaCase;

// namespace sps.Domain.Model.Dtos.EduStatus
// {
//     /// <summary>
//     /// Basic DTO for education status data
//     /// </summary>
//     public class EduStatusDto
//     {
//         /// <summary>
//         /// The unique identifier for the status
//         /// </summary>
//         public Guid Id { get; set; }
        
//         /// <summary>
//         /// The name of the status
//         /// </summary>
//         public string Name { get; set; }
        
//         /// <summary>
//         /// Number of active cases with this status
//         /// </summary>
//         public int ActiveCaseCount { get; set; }
//     }

//     /// <summary>
//     /// DTO for creating a new education status
//     /// </summary>
//     public class CreateEduStatusDto
//     {
//         /// <summary>
//         /// The name of the status
//         /// </summary>
//         [Required]
//         [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
//         public string Name { get; set; }
//     }

//     /// <summary>
//     /// DTO for updating an existing education status
//     /// </summary>
//     public class UpdateEduStatusDto
//     {
//         /// <summary>
//         /// The unique identifier of the status
//         /// </summary>
//         [Required]
//         public Guid Id { get; set; }
        
//         /// <summary>
//         /// The name of the status
//         /// </summary>
//         [Required]
//         [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
//         public string Name { get; set; }
//     }

//     /// <summary>
//     /// Detailed DTO for education status including related data
//     /// </summary>
//     public class EduStatusDetailDto
//     {
//         /// <summary>
//         /// Basic status information
//         /// </summary>
//         public EduStatusDto BasicInfo { get; set; }
        
//         /// <summary>
//         /// Cases with this status
//         /// </summary>
//         public List<SpsaCaseSummaryDto> Cases { get; set; }
        
//         /// <summary>
//         /// Statistics by education category
//         /// </summary>
//         public List<CategoryStats> StatsByCategory { get; set; }
        
//         public EduStatusDetailDto()
//         {
//             Cases = new List<SpsaCaseSummaryDto>();
//             StatsByCategory = new List<CategoryStats>();
//         }
//     }

//     /// <summary>
//     /// Statistics about cases in an education category
//     /// </summary>
//     public class CategoryStats
//     {
//         /// <summary>
//         /// Name of the education category
//         /// </summary>
//         public string CategoryName { get; set; }
        
//         /// <summary>
//         /// Number of active cases in this category
//         /// </summary>
//         public int ActiveCaseCount { get; set; }
        
//         /// <summary>
//         /// Total number of cases in this category
//         /// </summary>
//         public int TotalCaseCount { get; set; }
        
//         /// <summary>
//         /// Average hours sought per case in this category
//         /// </summary>
//         public double AverageHoursSought { get; set; }
//     }
// }