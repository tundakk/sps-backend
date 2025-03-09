using System;

namespace sps.Domain.Model.Dtos.SpsaCase
{
    /// <summary>
    /// DTO for SpsaCase comments
    /// </summary>
    public class SpsaCaseCommentDto
    {
        /// <summary>
        /// The unique identifier for the comment
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The comment text
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// When the comment was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}