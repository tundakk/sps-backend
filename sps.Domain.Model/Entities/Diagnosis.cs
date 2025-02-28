namespace sps.Domain.Model.Entities
{
    
 public class Diagnosis
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Navigation
    public ICollection<SpsaCase> SpsaCases { get; set; }
}
}