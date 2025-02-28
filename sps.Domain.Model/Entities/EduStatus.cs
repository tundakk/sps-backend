namespace sps.Domain.Model.Entities
{
    
   public class EduStatus
{
    public int Id { get; set; }
    public string Status { get; set; }
    
    // Navigation
    public ICollection<SpsaCase> SpsaCases { get; set; }
}
}