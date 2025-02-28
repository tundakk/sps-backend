namespace sps.Domain.Model.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TimeslotModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid RoomId { get; set; } // Foreign key to Room

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public bool IsAvailable { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}