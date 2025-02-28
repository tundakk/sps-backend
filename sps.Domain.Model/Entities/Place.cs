namespace sps.Domain.Model.Entities
{
    
 public class Place
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PlaceNumber { get; set; }
    public string Alias { get; set; }
    
    // Navigation
    public ICollection<SupportingTeacher> SupportingTeachers { get; set; }
}
}