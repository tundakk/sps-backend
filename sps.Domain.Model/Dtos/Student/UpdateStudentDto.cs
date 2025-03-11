// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;

// namespace sps.Domain.Model.Dtos.Student
// {
//     /// <summary>
//     /// DTO for updating an existing student
//     /// </summary>
//     public class UpdateStudentDto
//     {
//         /// <summary>
//         /// The unique identifier for the student
//         /// </summary>
//         [Required]
//         public Guid Id { get; set; }
        
//         /// <summary>
//         /// The student's academic identification number
//         /// </summary>
//         [Required]
//         [StringLength(50, MinimumLength = 1, ErrorMessage = "Student number must be between 1 and 50 characters")]
//         public string StudentNumber { get; set; }
        
//         /// <summary>
//         /// The student's name
//         /// </summary>
//         [Required]
//         [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
//         public string Name { get; set; }
        
//         /// <summary>
//         /// Comments about the student
//         /// </summary>
//         public List<string> Comments { get; set; } = new();
        
//         /// <summary>
//         /// The date when the student completed their education (if applicable)
//         /// </summary>
//         public DateTime? FinishedDate { get; set; }
        
//         /// <summary>
//         /// The ID of the period when the student started
//         /// </summary>
//         public Guid? StartPeriodId { get; set; }
        
//         /// <summary>
//         /// The ID of the student's education program
//         /// </summary>
//         public Guid? EducationId { get; set; }
//     }
// }