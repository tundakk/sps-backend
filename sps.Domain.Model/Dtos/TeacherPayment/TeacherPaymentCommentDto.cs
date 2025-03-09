using System;

namespace sps.Domain.Model.Dtos.TeacherPayment
{
    public class TeacherPaymentCommentDto
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}