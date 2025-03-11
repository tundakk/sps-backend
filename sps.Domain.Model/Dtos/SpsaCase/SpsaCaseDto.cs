// using System;
// using System.Collections.Generic;

// namespace sps.Domain.Model.Dtos.SpsaCase
// {
//     /// <summary>
//     /// Basic DTO for SpsaCase data used in list views and simple read operations
//     /// </summary>
//     public class SpsaCaseDto
//     {
//         /// <summary>
//         /// The unique identifier for the SPSA case
//         /// </summary>
//         public Guid Id { get; set; }

//         /// <summary>
//         /// The case reference number
//         /// </summary>
//         public string SpsaCaseNumber { get; set; }

//         /// <summary>
//         /// Number of hours requested for support
//         /// </summary>
//         public int HoursSought { get; set; }

//         /// <summary>
//         /// Number of hours actually spent
//         /// </summary>
//         public int HoursSpent { get; set; }

//         /// <summary>
//         /// Whether the case is currently active
//         /// </summary>
//         public bool IsActive { get; set; }

//         /// <summary>
//         /// The date when the application was submitted
//         /// </summary>
//         public DateTime? ApplicationDate { get; set; }

//         /// <summary>
//         /// The date when the case was last reapplied
//         /// </summary>
//         public DateTime? LatestReapplicationDate { get; set; }

//         /// <summary>
//         /// Primary comment about the case (legacy field)
//         /// </summary>
//         public string Comment { get; set; }

//         /// <summary>
//         /// List of comments about the case
//         /// </summary>
//         public List<SpsaCaseCommentDto> Comments { get; set; } = new();

//         /// <summary>
//         /// The ID of the student this case belongs to
//         /// </summary>
//         public Guid StudentId { get; set; }

//         /// <summary>
//         /// The name of the student
//         /// </summary>
//         public string StudentName { get; set; }

//         /// <summary>
//         /// The student's academic identification number
//         /// </summary>
//         public string StudentNumber { get; set; }

//         /// <summary>
//         /// The name of the student's education program
//         /// </summary>
//         public string EducationName { get; set; }

//         /// <summary>
//         /// The name of the period when the student started their education
//         /// </summary>
//         public string EducationStartPeriod { get; set; }

//         /// <summary>
//         /// The date when the student completed their education (if applicable)
//         /// </summary>
//         public DateTime? EducationFinishDate { get; set; }

//         /// <summary>
//         /// The ID of the supporting teacher (if assigned)
//         /// </summary>
//         public Guid? SupportingTeacherId { get; set; }

//         /// <summary>
//         /// The name of the supporting teacher
//         /// </summary>
//         public string SupportingTeacherName { get; set; }

//         /// <summary>
//         /// The email of the supporting teacher
//         /// </summary>
//         public string SupportingTeacherEmail { get; set; }

//         /// <summary>
//         /// The name of the place (institution) where the support is provided
//         /// </summary>
//         public string PlaceName { get; set; }

//         /// <summary>
//         /// The place number of the institution
//         /// </summary>
//         public string PlaceNumber { get; set; }

//         /// <summary>
//         /// The alias or alternate name of the place
//         /// </summary>
//         public string PlaceAlias { get; set; }

//         /// <summary>
//         /// The ID of the period when the application was made
//         /// </summary>
//         public Guid? AppliedPeriodId { get; set; }

//         /// <summary>
//         /// The ID of the diagnosis (if applicable)
//         /// </summary>
//         public Guid? DiagnosisId { get; set; }

//         /// <summary>
//         /// The name of the diagnosis
//         /// </summary>
//         public string DiagnosisName { get; set; }

//         /// <summary>
//         /// The ID of the education category
//         /// </summary>
//         public Guid? EduCategoryId { get; set; }

//         /// <summary>
//         /// The name of the education category
//         /// </summary>
//         public string EduCategoryName { get; set; }

//         /// <summary>
//         /// The ID of the support type
//         /// </summary>
//         public Guid? SupportTypeId { get; set; }

//         /// <summary>
//         /// The name of the support type
//         /// </summary>
//         public string SupportTypeName { get; set; }

//         /// <summary>
//         /// The ID of the education status
//         /// </summary>
//         public Guid? EduStatusId { get; set; }

//         /// <summary>
//         /// The name/description of the education status
//         /// </summary>
//         public string EduStatusName { get; set; }

//         /// <summary>
//         /// Whether course description was received
//         /// </summary>
//         public bool CourseDescriptionReceived { get; set; }

//         /// <summary>
//         /// Whether timesheet was received
//         /// </summary>
//         public bool TimesheetReceived { get; set; }

//         /// <summary>
//         /// Whether refund has been released for the student
//         /// </summary>
//         public bool StudentRefundReleased { get; set; }

//         /// <summary>
//         /// Whether refund has been released for the teacher
//         /// </summary>
//         public bool TeacherRefundReleased { get; set; }

//         /// <summary>
//         /// The support rate applicable for this case
//         /// </summary>
//         public decimal SupportRate { get; set; }

//         /// <summary>
//         /// Amount paid to the student
//         /// </summary>
//         public decimal StudentSupportSum { get; set; }

//         /// <summary>
//         /// Amount paid to the teacher
//         /// </summary>
//         public decimal TeacherSupportSum { get; set; }

//         /// <summary>
//         /// Total amount paid (student + teacher)
//         /// </summary>
//         public decimal TotalSum { get; set; }
//     }
// }