// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;

// namespace sps.Domain.Model.Dtos.OpkvalSupervision
// {
//     /// <summary>
//     /// DTO for creating a new supervision record
//     /// </summary>
//     public class CreateOpkvalSupervisionDto
//     {
//         /// <summary>
//         /// The date of supervision
//         /// </summary>
//         [Required]
//         public DateTime Date { get; set; }

//         /// <summary>
//         /// Name of the supervisor
//         /// </summary>
//         [Required]
//         [StringLength(100)]
//         public string SupervisorName { get; set; }

//         /// <summary>
//         /// Notes about the supervision
//         /// </summary>
//         public string Notes { get; set; }

//         /// <summary>
//         /// Outcome of the supervision
//         /// </summary>
//         public string Outcome { get; set; }

//         /// <summary>
//         /// Comments about the supervision
//         /// </summary>
//         public List<string> Comments { get; set; } = new();

//         /// <summary>
//         /// Hours requested for qualification/supervision
//         /// </summary>
//         [Required]
//         [Range(0, int.MaxValue)]
//         public int HoursSought { get; set; }

//         /// <summary>
//         /// Hours spent on qualification
//         /// </summary>
//         [Required]
//         [Range(0, int.MaxValue)]
//         public int QualificationHoursSpent { get; set; }

//         /// <summary>
//         /// Hours spent on supervision
//         /// </summary>
//         [Required]
//         [Range(0, int.MaxValue)]
//         public int SupervisionHoursSpent { get; set; }

//         /// <summary>
//         /// Current status
//         /// </summary>
//         [Required]
//         public string Status { get; set; }

//         /// <summary>
//         /// The IDs of the SPSA cases involved
//         /// </summary>
//         [Required]
//         public List<Guid> SpsaCaseIds { get; set; } = new();
//     }
// }