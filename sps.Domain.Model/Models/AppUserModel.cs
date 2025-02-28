namespace sps.Domain.Model.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AppUserModel
    {
        public Guid Id { get; set; }  // User ID (from IdentityUser)

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [Range(1, 999)]
        public int ApartmentNumber { get; set; }

        [Phone]
        public string? PhoneNumberSecondary { get; set; }
        public bool EmailOptOut { get; set; }
        public bool SmsOptOut { get; set; }

        [Range(1000, 9999)]
        public int? PinCode { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}