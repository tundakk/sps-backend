using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class IdentityUserModel
    {
        public Guid Id { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        public bool EmailConfirmed { get; set; }
    }
}