using sps.Domain.Model.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class StudentCommentModel
    {
        public Guid Id { get; set; }
        public SensitiveString CommentText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid StudentId { get; set; }
        public StudentModel Student { get; set; }
    }
}