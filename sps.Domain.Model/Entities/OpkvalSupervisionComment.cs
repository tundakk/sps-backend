using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Entities
{
    public class OpkvalSupervisionComment
    {
        public Guid Id { get; set; }
        public SensitiveString CommentText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public Guid OpkvalSupervisionId { get; set; }
        public OpkvalSupervision OpkvalSupervision { get; set; }
    }
}