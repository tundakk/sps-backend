using System;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos.Student
{
    /// <summary>
    /// DTO for adding a comment to an existing student
    /// </summary>
    public class AddStudentCommentDto
    {
        /// <summary>
        /// The ID of the student
        /// </summary>
        [Required]
        public Guid StudentId { get; set; }

        /// <summary>
        /// The comment text
        /// </summary>
        [Required]
        [StringLength(500)]
        public string CommentText { get; set; }
    }
}