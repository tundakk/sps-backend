using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos
{
    /// <summary>
    /// Data transfer object for user registration
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Email address of the user (used as username)
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        /// <summary>
        /// Password confirmation
        /// </summary>
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}