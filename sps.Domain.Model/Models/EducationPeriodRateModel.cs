using System;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class EducationPeriodRateModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public Guid PeriodId { get; set; }
        public PeriodModel Period { get; set; }
        
        // Update property names to be consistent with entity
        [Required]
        public Guid EducationalProgramId { get; set; }
        public EducationalProgramModel EducationalProgram { get; set; }
    }
}