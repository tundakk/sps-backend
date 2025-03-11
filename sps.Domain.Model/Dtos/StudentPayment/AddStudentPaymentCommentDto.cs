// using System;
// using System.ComponentModel.DataAnnotations;

// namespace sps.Domain.Model.Dtos.StudentPayment
// {
//     /// <summary>
//     /// DTO for adding a comment to an existing student payment
//     /// </summary>
//     public class AddStudentPaymentCommentDto
//     {
//         /// <summary>
//         /// The ID of the student payment
//         /// </summary>
//         [Required]
//         public Guid StudentPaymentId { get; set; }

//         /// <summary>
//         /// The comment text
//         /// </summary>
//         [Required]
//         [StringLength(500)]
//         public string CommentText { get; set; }
//     }
// }