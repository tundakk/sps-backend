using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Entities
{
    public class StudentComment
    {
        public Guid Id { get; set; }
        
        public SensitiveString CommentText { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign key for Student
        public Guid StudentId { get; set; }
        
        // Navigation property
        public Student Student { get; set; }
    }
}