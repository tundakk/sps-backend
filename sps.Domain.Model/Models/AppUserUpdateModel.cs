using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class AppUserUpdateModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 256 characters.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public required string Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

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