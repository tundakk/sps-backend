using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Entities
{
    /// <summary>
    /// Represents a comment that can be associated with various entities in the system
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// The unique identifier for the comment
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The text content of the comment
        /// </summary>
        public required SensitiveString CommentText { get; set; }

        /// <summary>
        /// The timestamp when the comment was created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The user who created this comment
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// The type of entity this comment is associated with
        /// </summary>
        public required string EntityType { get; set; }

        /// <summary>
        /// Optional reference to an SpsaCase if this comment is associated with one
        /// </summary>
        public Guid? SpsaCaseId { get; set; }

        /// <summary>
        /// Navigation property for the associated SpsaCase
        /// </summary>
        public SpsaCase? SpsaCase { get; set; }

        /// <summary>
        /// Optional reference to a Student if this comment is associated with one
        /// </summary>
        public Guid? StudentId { get; set; }

        /// <summary>
        /// Navigation property for the associated Student
        /// </summary>
        public Student? Student { get; set; }

        /// <summary>
        /// Optional reference to a TeacherPayment if this comment is associated with one
        /// </summary>
        public Guid? TeacherPaymentId { get; set; }

        /// <summary>
        /// Navigation property for the associated TeacherPayment
        /// </summary>
        public TeacherPayment? TeacherPayment { get; set; }

        /// <summary>
        /// Optional reference to a StudentPayment if this comment is associated with one
        /// </summary>
        public Guid? StudentPaymentId { get; set; }

        /// <summary>
        /// Navigation property for the associated StudentPayment
        /// </summary>
        public StudentPayment? StudentPayment { get; set; }

        /// <summary>
        /// Optional reference to an OpkvalSupervision if this comment is associated with one
        /// </summary>
        public Guid? OpkvalSupervisionId { get; set; }

        /// <summary>
        /// Navigation property for the associated OpkvalSupervision
        /// </summary>
        public OpkvalSupervision? OpkvalSupervision { get; set; }
    }
}