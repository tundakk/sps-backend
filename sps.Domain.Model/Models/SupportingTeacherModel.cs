using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Models
{
    public class SupportingTeacherModel
    {
        public SupportingTeacherModel()
        {
            SpsaCases = new HashSet<SpsaCaseModel>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required SensitiveString Email { get; set; }
        
        // Replace PlacesId with EducationalProgramId
        public Guid? EducationalProgramId { get; set; }
        public EducationalProgramModel? EducationalProgram { get; set; }
        
        public ICollection<SpsaCaseModel> SpsaCases { get; init; }
    }
}