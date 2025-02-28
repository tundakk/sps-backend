using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class UserProfileModel
    {
        public Guid Id { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        [Range(1, 999)]
        public int ApartmentNumber { get; set; }
        
        [Phone]
        public string? PhoneNumberSecondary { get; set; }
        
        public bool EmailOptOut { get; set; }
        public bool SmsOptOut { get; set; }
        
        [Range(1000, 9999)]
        public int? PinCode { get; set; }
    }
}