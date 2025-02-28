namespace sps.Domain.Model.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AppUserCreateModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [Range(1, 999)]
        public int ApartmentNumber { get; set; }

        [Phone]
        public string? PhoneNumberSecondary { get; set; }
    }
}