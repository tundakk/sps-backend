// using System;
// using System.ComponentModel.DataAnnotations;

// namespace sps.Domain.Model.Dtos.SpsaCase
// {
//     /// <summary>
//     /// DTO for adding a comment to an existing SPSA case
//     /// </summary>
//     public class AddSpsaCaseCommentDto
//     {
//         /// <summary>
//         /// The ID of the SPSA case
//         /// </summary>
//         [Required]
//         public Guid SpsaCaseId { get; set; }

//         /// <summary>
//         /// The comment text
//         /// </summary>
//         [Required]
//         [StringLength(500)]
//         public string CommentText { get; set; }
//     }
// }