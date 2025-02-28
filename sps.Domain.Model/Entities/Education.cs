namespace sps.Domain.Model.Entities
{
   public class Education
{
    public int Id { get; set; }
    public string Name { get; set; } // e.g. "Bioanalytiker"
    
    // Foreign key
    public int EduCategoryId { get; set; }
    public EduCategory EduCategory { get; set; }
    
    // Navigation
    public ICollection<EducationPeriodRate> EducationPeriodRates { get; set; }
    public ICollection<Student> Students { get; set; }
}
}