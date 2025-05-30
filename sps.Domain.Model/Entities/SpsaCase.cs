﻿using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Entities
{
    // SpsaCase.cs
    public class SpsaCase
    {
        public SpsaCase()
        {
            Comments = new HashSet<Comment>();
        }

        public Guid Id { get; set; }
        public required string SpsaCaseNumber { get; set; }
        public int HoursSought { get; set; }
        public int HoursSpent { get; set; }
        
        public ICollection<Comment> Comments { get; init; }
        
        public bool IsActive { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? LatestReapplicationDate { get; set; }    
        /// <summary>
        ///Forløbsbeskrivelse er modtaget
        /// </summary>
        public bool CourseDescriptionReceived { get; set; }
        public bool TimesheetReceived { get; set; }
        public bool StudentRefundReleased { get; set; }
        public bool TeacherRefundReleased { get; set; }
        public decimal SupportRate { get; set; }

        public required Guid StudentId { get; set; }
        public required Student Student { get; set; }

        public Guid? SupportingTeacherId { get; set; }
        public SupportingTeacher? SupportingTeacher { get; set; }

        public Guid? AppliedPeriodId { get; set; }
        public Period? AppliedPeriod { get; set; }

        public Guid? DiagnosisId { get; set; }
        public Diagnosis? Diagnosis { get; set; }

        public Guid? EduCategoryId { get; set; }
        public EduCategory? EduCategory { get; set; }

        public Guid? SupportTypeId { get; set; }
        public SupportType? SupportType { get; set; }

        public Guid? EduStatusId { get; set; }
        public EduStatus? EduStatus { get; set; }

        public Guid? TeacherPaymentId { get; set; }
        public TeacherPayment? TeacherPayment { get; set; }

        public Guid? OpkvalSupervisionId { get; set; }
        public OpkvalSupervision? OpkvalSupervision { get; set; }

        public Guid? StudentPaymentId { get; set; }
        public StudentPayment? StudentPayment { get; set; }
    }
}