using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class DesiredTimeslotModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid TimeslotId { get; set; }

        public bool NotificationSent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}