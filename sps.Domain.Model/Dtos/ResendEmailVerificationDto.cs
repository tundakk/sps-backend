using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos
{
    /// <summary>
    /// DTO for resending email verification code
    /// </summary>
    public class ResendEmailVerificationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}