using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos
{
    /// <summary>
    /// Data transfer object for login credentials
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Email address of the user
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}