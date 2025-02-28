namespace sps.Domain.Model.Entities
{
    
public class Student
{
    public int Id { get; set; }
    public string StudentNumber { get; set; }
    public string CPRNumber { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public DateTime? FinishedDate { get; set; } // store (month/year) or full date
    
    // FKs
    public int? StartPeriodId { get; set; }
    public Period StartPeriod { get; set; }
    
    public int? EducationId { get; set; }
    public Education Education { get; set; }
    
    // Navigation
    public ICollection<SpsaCase> SpsaCases { get; set; }
}
}