using System;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos.OpkvalSupervision
{
    /// <summary>
    /// DTO for adding a comment to an existing supervision record
    /// </summary>
    public class AddOpkvalSupervisionCommentDto
    {
        /// <summary>
        /// The ID of the supervision record
        /// </summary>
        [Required]
        public Guid OpkvalSupervisionId { get; set; }

        /// <summary>
        /// The comment text
        /// </summary>
        [Required]
        [StringLength(500)]
        public string CommentText { get; set; }
    }
}