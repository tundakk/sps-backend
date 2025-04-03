using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class EduCategoryModel
    {
        public EduCategoryModel()
        {
            EducationalPrograms = new HashSet<EducationalProgramModel>();
            SpsaCases = new HashSet<SpsaCaseModel>();
        }

        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<EducationalProgramModel> EducationalPrograms { get; init; }
        public ICollection<SpsaCaseModel> SpsaCases { get; init; }
    }
}