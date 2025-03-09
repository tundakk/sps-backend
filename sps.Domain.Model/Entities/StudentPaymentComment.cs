using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Entities
{
    public class StudentPaymentComment
    {
        public Guid Id { get; set; }
        public SensitiveString CommentText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public Guid StudentPaymentId { get; set; }
        public StudentPayment StudentPayment { get; set; }
    }
}