// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using System.Text.Json.Serialization;

// namespace sps.Domain.Model.Dtos.Student
// {
//     /// <summary>
//     /// DTO for Student data used in read operations and API responses
//     /// </summary>
//     public class StudentDto
//     {
//         /// <summary>
//         /// The unique identifier for the student
//         /// </summary>
//         public Guid Id { get; set; }
        
//         /// <summary>
//         /// The student's academic identification number
//         /// </summary>
//         public string StudentNumber { get; set; }
        
//         /// <summary>
//         /// The student's name
//         /// </summary>
//         public string Name { get; set; }
        
//         /// <summary>
//         /// Comments about the student
//         /// </summary>
//         public List<StudentCommentDto> Comments { get; set; } = new();
        
//         /// <summary>
//         /// The date when the student completed their education (if applicable)
//         /// </summary>
//         public DateTime? FinishedDate { get; set; }
        
//         /// <summary>
//         /// The period ID when the student started
//         /// </summary>
//         public Guid? StartPeriodId { get; set; }
        
//         /// <summary>
//         /// The name of the start period (included for display purposes)
//         /// </summary>
//         public string PeriodName { get; set; }
        
//         /// <summary>
//         /// The ID of the student's education program
//         /// </summary>
//         public Guid? EducationId { get; set; }
        
//         /// <summary>
//         /// The name of the student's education program (included for display purposes)
//         /// </summary>
//         public string EducationName { get; set; }
//     }
// }