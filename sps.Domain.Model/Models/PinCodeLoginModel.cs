using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    /// <summary>
    /// Model for pin code login.
    /// </summary>
    public class PinCodeLoginModel
    {
        /// <summary>
        /// Gets or sets the apartment number.
        /// </summary>
        [Required]
        [Range(1, 999)]
        public int ApartmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the pin code.
        /// </summary>
        [Required]
        [Range(1000, 9999)]
        public int PinCode { get; set; }
    }
}