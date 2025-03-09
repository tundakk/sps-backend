namespace sps.Domain.Model.Dtos.SpsaCase
{
    using System;
    using System.Collections.Generic;
    using sps.Domain.Model.Dtos.StudentPayment;
    using sps.Domain.Model.Dtos.TeacherPayment;
    using sps.Domain.Model.Dtos.OpkvalSupervision;

    /// <summary>
    /// Detailed DTO for SpsaCase data including all related information
    /// </summary>
    public class SpsaCaseDetailDto
    {
        /// <summary>
        /// Basic case information
        /// </summary>
        public SpsaCaseDto BasicInfo { get; set; }

        /// <summary>
        /// Detailed information about the student
        /// </summary>
        public StudentInfo Student { get; set; }

        /// <summary>
        /// Information about the supporting teacher
        /// </summary>
        public TeacherInfo SupportingTeacher { get; set; }

        /// <summary>
        /// Period information
        /// </summary>
        public PeriodInfo AppliedPeriod { get; set; }

        /// <summary>
        /// Payment information for the student
        /// </summary>
        public PaymentInfo StudentPayment { get; set; }

        /// <summary>
        /// Payment information for the teacher
        /// </summary>
        public PaymentInfo TeacherPayment { get; set; }

        /// <summary>
        /// Information about supervision
        /// </summary>
        public SupervisionInfo Supervision { get; set; }
        
        /// <summary>
        /// List of comments about the case
        /// </summary>
        public List<SpsaCaseCommentDto> Comments { get; set; } = new List<SpsaCaseCommentDto>();
        
        /// <summary>
        /// Primary comment about the case
        /// </summary>
        public string Comment { get; set; }
        
        /// <summary>
        /// Whether course description was received
        /// </summary>
        public bool CourseDescriptionReceived { get; set; }
        
        /// <summary>
        /// Whether timesheet was received
        /// </summary>
        public bool TimesheetReceived { get; set; }
        
        /// <summary>
        /// Whether refund has been released for the student
        /// </summary>
        public bool StudentRefundReleased { get; set; }
        
        /// <summary>
        /// Whether refund has been released for the teacher
        /// </summary>
        public bool TeacherRefundReleased { get; set; }
    }

    /// <summary>
    /// Basic student information for SPSA case details
    /// </summary>
    public class StudentInfo
    {
        public Guid Id { get; set; }
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public string CPRNumber { get; set; }
        public string Education { get; set; }
        public string StartPeriodName { get; set; }
        public DateTime? FinishedDate { get; set; }
    }

    /// <summary>
    /// Supporting teacher information for SPSA case details
    /// </summary>
    public class TeacherInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string PlaceNumber { get; set; }
        public string PlaceAlias { get; set; }
        public string Email { get; set; }
    }

    /// <summary>
    /// Period information for SPSA case details
    /// </summary>
    public class PeriodInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Payment information for SPSA case details
    /// </summary>
    public class PaymentInfo
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string AccountNumber { get; set; }
        public List<CommentInfo> Comments { get; set; } = new List<CommentInfo>();
        public string ExternalVoucherNumber { get; set; }
        public string VoucherText { get; set; }
        public string CompleteVoucherText { get; set; }
    }

    /// <summary>
    /// Supervision information for SPSA case details
    /// </summary>
    public class SupervisionInfo
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Supervisor { get; set; }
        public string Notes { get; set; }
        public string Outcome { get; set; }
        
        /// <summary>
        /// Number of hours requested for qualification/supervision
        /// </summary>
        public int HoursSought { get; set; }
        
        /// <summary>
        /// Number of hours actually spent on qualification
        /// </summary>
        public int QualificationHoursSpent { get; set; }
        
        /// <summary>
        /// Number of hours actually spent on supervision
        /// </summary>
        public int SupervisionHoursSpent { get; set; }
        
        /// <summary>
        /// Status of qualification/supervision
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Comments about the qualification/supervision
        /// </summary>
        public List<CommentInfo> Comments { get; set; } = new List<CommentInfo>();
    }

    /// <summary>
    /// Generic comment information
    /// </summary>
    public class CommentInfo
    {
        /// <summary>
        /// The unique identifier for the comment
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The comment text
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// When the comment was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}