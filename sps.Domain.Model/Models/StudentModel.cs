using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.Entities;
using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Models
{
    public class StudentModel
    {
        public StudentModel()
        {
            SpsaCases = new HashSet<SpsaCaseModel>();
            Comments = new HashSet<Comment>();
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
        public ICollection<Comment> Comments { get; init; }
        
        public DateTime? FinishedDate { get; set; }
        
        public Guid? StartPeriodId { get; set; }
        public PeriodModel? StartPeriod { get; set; }
        
        public Guid? EducationId { get; set; }
        public EducationModel? Education { get; set; }
        
        public ICollection<SpsaCaseModel> SpsaCases { get; init; }
    }
}