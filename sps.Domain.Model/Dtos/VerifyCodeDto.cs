using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos
{
    /// <summary>
    /// Data transfer object for two-factor authentication code verification
    /// </summary>
    public class VerifyCodeDto
    {
        /// <summary>
        /// Email address of the user
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Verification code sent to the user's phone
        /// </summary>
        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string Code { get; set; }
    }
}