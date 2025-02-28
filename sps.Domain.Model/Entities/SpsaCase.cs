namespace sps.Domain.Model.Entities
{
    // SpsaCase.cs
public class SpsaCase
{
    public int Id { get; set; }
    public string SpsaCaseNumber { get; set; }
    public decimal HoursSought { get; set; }
    public decimal HoursSpent { get; set; }
    public string Comment { get; set; }
    public bool IsActive { get; set; }
    public DateTime? ApplicationDate { get; set; }
    public DateTime? LatestReapplicationDate { get; set; }
    
    // FKs
    public int StudentId { get; set; }
    public Student Student { get; set; }
    
    public int? SupportingTeacherId { get; set; }
    public SupportingTeacher SupportingTeacher { get; set; }
    
    public int? AppliedPeriodId { get; set; }
    public Period AppliedPeriod { get; set; }
    
    public int? DiagnosisId { get; set; }
    public Diagnosis Diagnosis { get; set; }
    
    public int? EduCategoryId { get; set; }
    public EduCategory EduCategory { get; set; }
    
    public int? SupportTypeId { get; set; }
    public SupportType SupportType { get; set; }
    
    public int? EduStatusId { get; set; }
    public EduStatus EduStatus { get; set; }
    
    public int? TeacherPaymentId { get; set; }
    public TeacherPayment TeacherPayment { get; set; }
    
    public int? OpkvalSupervisionId { get; set; }
    public OpkvalSupervision OpkvalSupervision { get; set; }
    
    public int? StudentPaymentId { get; set; }
    public StudentPayment StudentPayment { get; set; }
}
}