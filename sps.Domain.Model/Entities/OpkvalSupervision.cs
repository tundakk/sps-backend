namespace sps.Domain.Model.Entities
{
    
   public class OpkvalSupervision
{
    public int Id { get; set; }
    public string Status { get; set; }
    public decimal HoursSpentSupervision { get; set; }
    public decimal HoursSpentOpkvalificering { get; set; }
    public decimal HoursSought { get; set; }
    
    // Navigation
    public ICollection<SpsaCase> SpsaCases { get; set; }
}
}