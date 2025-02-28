namespace sps.Domain.Model.Entities
{
    
public class EduCategory
{
    // e.g. Professionsbachelor, Erhvervsakademiuddannelse
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Navigation
    public ICollection<Education> Educations { get; set; }
    public ICollection<SpsaCase> SpsaCases { get; set; }
}
}