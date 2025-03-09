namespace sps.Domain.Model.Entities
{
    public class SpsaCaseComment
    {
        public Guid Id { get; set; }
        
        public string CommentText { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign key for SpsaCase
        public Guid SpsaCaseId { get; set; }
        
        // Navigation property
        public SpsaCase SpsaCase { get; set; }
    }
}