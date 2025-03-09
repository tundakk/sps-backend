using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Models
{
    public class StudentModel
    {
        public StudentModel()
        {
            SpsaCases = new HashSet<SpsaCaseModel>();
            Comments = new HashSet<StudentCommentModel>();
        }

        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public required string StudentNumber { get; set; }
        
        [Required]
        public required CPRNumber CPRNumber { get; set; }
        
        [Required]
        public required SensitiveString Name { get; set; }
        
        [MaxLength(500)]
        public SensitiveString? Comment { get; set; }
        
        public DateTime? FinishedDate { get; set; }
        
        public Guid? StartPeriodId { get; set; }
        public PeriodModel? StartPeriod { get; set; }
        
        public Guid? EducationId { get; set; }
        public EducationModel? Education { get; set; }
        
        public ICollection<SpsaCaseModel> SpsaCases { get; init; }
        public ICollection<StudentCommentModel> Comments { get; init; }
    }
}