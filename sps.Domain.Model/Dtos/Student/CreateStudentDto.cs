// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;

// namespace sps.Domain.Model.Dtos.Student
// {
//     /// <summary>
//     /// DTO for creating a new student
//     /// </summary>
//     public class CreateStudentDto
//     {
//         /// <summary>
//         /// The student's academic identification number
//         /// </summary>
//         [Required]
//         [StringLength(50, MinimumLength = 1, ErrorMessage = "Student number must be between 1 and 50 characters")]
//         public string StudentNumber { get; set; }
        
//         /// <summary>
//         /// The student's CPR number (Danish personal ID)
//         /// </summary>
//         [Required]
//         [RegularExpression(@"^\d{6}-\d{4}$", ErrorMessage = "CPR number must be in the format 'DDMMYY-XXXX'")]
//         public string CPRNumber { get; set; }
        
//         /// <summary>
//         /// The student's name
//         /// </summary>
//         [Required]
//         [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
//         public string Name { get; set; }
        
//         /// <summary>
//         /// Initial comments about the student
//         /// </summary>
//         public List<string> Comments { get; set; } = new();
        
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