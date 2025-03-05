using sps.Domain.Model.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class SpsaCaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string SpsaCaseNumber { get; set; }

        [Required]
        public int HoursSought { get; set; }

        [Required]
        public int HoursSpent { get; set; }

        [MaxLength(500)]
        public SensitiveString Comment { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public DateTime? ApplicationDate { get; set; }
        public DateTime? LatestReapplicationDate { get; set; }

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
}