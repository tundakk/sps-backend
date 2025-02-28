namespace sps.Domain.Model.Entities
{
    
public class EducationPeriodRate
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    
    // FKs
    public int PeriodId { get; set; }
    public Period Period { get; set; }
    
    public int EducationId { get; set; }
    public Education Education { get; set; }
}
}