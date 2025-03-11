using sps.Domain.Model.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    /// <summary>
    /// General purpose comment model that can be associated with various entity types
    /// </summary>
    public class CommentModel
    {
        /// <summary>
        /// Unique identifier for the comment
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The text content of the comment (sensitive information)
        /// </summary>
        [Required]
        public SensitiveString CommentText { get; set; }

        /// <summary>
        /// When the comment was created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Type of entity this comment is associated with
        /// </summary>
        [Required]
        public string EntityType { get; set; }

        /// <summary>
        /// ID of the entity this comment is associated with
        /// </summary>
        [Required]
        public Guid EntityId { get; set; }

        /// <summary>
        /// Optional user/author who created this comment
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Optional display name for the related entity
        /// </summary>
        public string? EntityName { get; set; }
    }
}