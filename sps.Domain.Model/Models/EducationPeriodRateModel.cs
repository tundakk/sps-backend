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
        
        [Required]
        public Guid EducationId { get; set; }
        public EducationModel Education { get; set; }
    }
}