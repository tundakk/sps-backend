using sps.Domain.Model.ValueObjects;
using System;

namespace sps.Domain.Model.Models
{
    public class OpkvalSupervisionCommentModel
    {
        public Guid Id { get; set; }
        public SensitiveString CommentText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid OpkvalSupervisionId { get; set; }
        public OpkvalSupervisionModel OpkvalSupervision { get; set; }
    }
}