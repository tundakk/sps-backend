using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class ChatMessageModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid SenderId { get; set; }

        [Required]
        public Guid ReceiverId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}