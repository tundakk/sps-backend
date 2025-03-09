using System;

namespace sps.Domain.Model.Dtos.OpkvalSupervision
{
    public class OpkvalSupervisionCommentDto
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}