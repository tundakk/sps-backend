// using System;
// using System.ComponentModel.DataAnnotations;

// namespace sps.Domain.Model.Dtos.Education
// {
//     /// <summary>
//     /// DTO for creating a new period rate
//     /// </summary>
//     public class CreatePeriodRateDto
//     {
//         /// <summary>
//         /// The ID of the education program
//         /// </summary>
//         [Required]
//         public Guid EducationId { get; set; }
        
//         /// <summary>
//         /// The ID of the period
//         /// </summary>
//         [Required]
//         public Guid PeriodId { get; set; }
        
//         /// <summary>
//         /// The rate to configure for this period
//         /// </summary>
//         [Required]
//         [Range(0, double.MaxValue, ErrorMessage = "Rate must be greater than or equal to 0")]
//         public decimal Rate { get; set; }
//     }

//     /// <summary>
//     /// DTO for updating an existing period rate
//     /// </summary>
//     public class UpdatePeriodRateDto
//     {
//         /// <summary>
//         /// The unique identifier of the rate configuration
//         /// </summary>
//         [Required]
//         public Guid Id { get; set; }
        
//         /// <summary>
//         /// The ID of the education program
//         /// </summary>
//         [Required]
//         public Guid EducationId { get; set; }
        
//         /// <summary>
//         /// The ID of the period
//         /// </summary>
//         [Required]
//         public Guid PeriodId { get; set; }
        
//         /// <summary>
//         /// The rate to configure for this period
//         /// </summary>
//         [Required]
//         [Range(0, double.MaxValue, ErrorMessage = "Rate must be greater than or equal to 0")]
//         public decimal Rate { get; set; }
//     }
// }