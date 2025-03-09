using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class EducationModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        public Guid EduCategoryId { get; set; }
        public EduCategoryModel EduCategory { get; set; }
        
        public ICollection<EducationPeriodRateModel> EducationPeriodRates { get; set; }
        public ICollection<StudentModel> Students { get; set; }
    }
}