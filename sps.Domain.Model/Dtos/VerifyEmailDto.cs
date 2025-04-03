using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos
{
    /// <summary>
    /// DTO for email verification
    /// </summary>
    public class VerifyEmailDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Code { get; set; } = string.Empty;
    }
}