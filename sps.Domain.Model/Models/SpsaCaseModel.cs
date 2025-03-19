using sps.Domain.Model.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class SpsaCaseModel
    {
        public SpsaCaseModel()
        {
            Comments = new HashSet<SpsaCaseCommentModel>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string SpsaCaseNumber { get; set; }

        [Required]
        public int HoursSought { get; set; }

        [Required]
        public int HoursSpent { get; set; }

        public SensitiveString Comment { get; set; }

        public ICollection<SpsaCaseCommentModel> Comments { get; init; } // TODO: change to commentmodel

        [Required]
        public bool IsActive { get; set; }

        public DateTime? ApplicationDate { get; set; }
        public DateTime? LatestReapplicationDate { get; set; }

        public bool CourseDescriptionReceived { get; set; }
        public bool TimesheetReceived { get; set; }
        public bool StudentRefundReleased { get; set; }
        public bool TeacherRefundReleased { get; set; }
        public decimal SupportRate { get; set; }

        [Required]
        public Guid StudentId { get; set; }

        public StudentModel Student { get; set; }

        public Guid? SupportingTeacherId { get; set; }
        public SupportingTeacherModel SupportingTeacher { get; set; }

        public Guid? AppliedPeriodId { get; set; }
        public PeriodModel AppliedPeriod { get; set; }

        public Guid? DiagnosisId { get; set; }
        public DiagnosisModel Diagnosis { get; set; }

        public Guid? EduCategoryId { get; set; }
        public EduCategoryModel EduCategory { get; set; }

        public Guid? SupportTypeId { get; set; }
        public SupportTypeModel SupportType { get; set; }

        public Guid? EduStatusId { get; set; }
        public EduStatusModel EduStatus { get; set; }

        public Guid? TeacherPaymentId { get; set; }
        public TeacherPaymentModel TeacherPayment { get; set; }

        public Guid? OpkvalSupervisionId { get; set; }
        public OpkvalSupervisionModel OpkvalSupervision { get; set; }

        public Guid? StudentPaymentId { get; set; }
        public StudentPaymentModel StudentPayment { get; set; }
    }

    public class SpsaCaseCommentModel
    {
        public Guid Id { get; set; }
        public SensitiveString CommentText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid SpsaCaseId { get; set; }
        public SpsaCaseModel SpsaCase { get; set; }
    }
}