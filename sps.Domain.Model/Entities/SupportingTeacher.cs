namespace sps.Domain.Model.Entities
{
    
public class SupportingTeacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    // FK
    public int? PlacesId { get; set; }
    public Place Place { get; set; }
    
    // Navigation
    public ICollection<SpsaCase> SpsaCases { get; set; }
}
}