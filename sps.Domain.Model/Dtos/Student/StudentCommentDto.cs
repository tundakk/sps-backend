using System;

namespace sps.Domain.Model.Dtos.Student
{
    public class StudentCommentDto
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}