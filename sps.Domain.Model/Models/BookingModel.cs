using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.Enums;

namespace sps.Domain.Model.Models
{
    public class BookingModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid TimeslotId { get; set; }

        [Required]
        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}