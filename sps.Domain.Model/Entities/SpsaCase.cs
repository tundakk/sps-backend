namespace sps.Domain.Model.Entities
{
    // SpsaCase.cs
    public class SpsaCase
    {
        public Guid Id { get; set; }
        public string SpsaCaseNumber { get; set; }
        public int HoursSought { get; set; }
        public int HoursSpent { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? LatestReapplicationDate { get; set; }

        // FKs
        public Guid StudentId { get; set; }

        public Student Student { get; set; }

        public Guid? SupportingTeacherId { get; set; }
        public SupportingTeacher SupportingTeacher { get; set; }

        public Guid? AppliedPeriodId { get; set; }
        public Period AppliedPeriod { get; set; }

        public Guid? DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public Guid? EduCategoryId { get; set; }
        public EduCategory EduCategory { get; set; }

        public Guid? SupportTypeId { get; set; }
        public SupportType SupportType { get; set; }

        public Guid? EduStatusId { get; set; }
        public EduStatus EduStatus { get; set; }

        public Guid? TeacherPaymentId { get; set; }
        public TeacherPayment TeacherPayment { get; set; }

        public Guid? OpkvalSupervisionId { get; set; }
        public OpkvalSupervision OpkvalSupervision { get; set; }

        public Guid? StudentPaymentId { get; set; }
        public StudentPayment StudentPayment { get; set; }
    }
}