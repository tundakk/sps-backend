using sps.Domain.Model.ValueObjects;
using System;

namespace sps.Domain.Model.Models
{
    public class StudentPaymentCommentModel
    {
        public Guid Id { get; set; }
        public SensitiveString CommentText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid StudentPaymentId { get; set; }
        public StudentPaymentModel StudentPayment { get; set; }
    }
}