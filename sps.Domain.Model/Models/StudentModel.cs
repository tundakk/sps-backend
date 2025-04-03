using sps.Domain.Model.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class StudentModel
    {
        public StudentModel()
        {
            SpsaCases = new HashSet<SpsaCaseModel>();
            Comments = new HashSet<CommentModel>();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
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
        public ICollection<CommentModel> Comments { get; init; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? FinishedDate { get; set; }

        public Guid? StartPeriodId { get; set; }
        public PeriodModel? StartPeriod { get; set; }

        public Guid? EducationalProgramId { get; set; }
        public EducationalProgramModel? EducationalProgram { get; set; }

        public ICollection<SpsaCaseModel> SpsaCases { get; init; }
    }
}