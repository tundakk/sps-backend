using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class PeriodModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        public ICollection<EducationPeriodRateModel> EducationPeriodRates { get; set; }
        public ICollection<SpsaCaseModel> SpsaCases { get; set; }
        public ICollection<StudentModel> Students { get; set; }
    }
}