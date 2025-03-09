using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos.Period
{
    /// <summary>
    /// DTO for creating a new academic period
    /// </summary>
    public class CreatePeriodDto
    {
        /// <summary>
        /// The name of the period (e.g., "F2023" for Fall 2023)
        /// </summary>
        [Required]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Period name must be between 4 and 10 characters")]
        [RegularExpression(@"^[FSV]\d{4}$", ErrorMessage = "Period name must start with F, S, or V followed by a 4-digit year")]
        public string Name { get; set; }
        
        /// <summary>
        /// The start date of the period
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// The end date of the period
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// Validates that the end date is after the start date
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult(
                    "End date must be after start date",
                    new[] { nameof(EndDate) }
                );
            }
        }
    }
}