using System;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos.TeacherPayment
{
    /// <summary>
    /// DTO for adding a comment to an existing teacher payment
    /// </summary>
    public class AddTeacherPaymentCommentDto
    {
        /// <summary>
        /// The ID of the teacher payment
        /// </summary>
        [Required]
        public Guid TeacherPaymentId { get; set; }

        /// <summary>
        /// The comment text
        /// </summary>
        [Required]
        [StringLength(500)]
        public string CommentText { get; set; }
    }
}