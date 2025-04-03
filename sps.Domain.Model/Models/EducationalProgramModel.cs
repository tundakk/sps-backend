using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.Entities;

namespace sps.Domain.Model.Models
{
    public class EducationalProgramModel
    {
        public EducationalProgramModel()
        {
            EducationPeriodRates = new HashSet<EducationPeriodRateModel>();
            Students = new HashSet<StudentModel>();
            SupportingTeachers = new HashSet<SupportingTeacherModel>();
        }

        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? ProgramCode { get; set; }
        
        [MaxLength(100)]
        public string? Alias { get; set; }
        
        public Guid EduCategoryId { get; set; }
        public EduCategoryModel? EduCategory { get; set; }

        // Navigation properties
        public ICollection<EducationPeriodRateModel> EducationPeriodRates { get; init; }
        public ICollection<StudentModel> Students { get; init; }
        public ICollection<SupportingTeacherModel> SupportingTeachers { get; init; }
    }
}