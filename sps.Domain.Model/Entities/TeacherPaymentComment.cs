using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Entities
{
    public class TeacherPaymentComment
    {
        public Guid Id { get; set; }
        public SensitiveString CommentText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public Guid TeacherPaymentId { get; set; }
        public TeacherPayment TeacherPayment { get; set; }
    }
}