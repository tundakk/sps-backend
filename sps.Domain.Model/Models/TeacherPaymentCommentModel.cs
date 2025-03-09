using sps.Domain.Model.ValueObjects;
using System;

namespace sps.Domain.Model.Models
{
    public class TeacherPaymentCommentModel
    {
        public Guid Id { get; set; }
        public SensitiveString CommentText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid TeacherPaymentId { get; set; }
        public TeacherPaymentModel TeacherPayment { get; set; }
    }
}