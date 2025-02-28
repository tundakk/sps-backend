using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class ServiceMessageModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(2000)]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}