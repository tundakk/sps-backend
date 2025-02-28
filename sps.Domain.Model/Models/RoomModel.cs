using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class RoomModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        [Range(1, 100)]
        public int MaxCapacity { get; set; }
    }
}