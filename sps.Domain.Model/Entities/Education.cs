namespace sps.Domain.Model.Entities
{
    public class Education
    {
        public Guid Id { get; set; }
        public string Name { get; set; } // e.g. "Bioanalytiker"

        // Foreign key
        public Guid EduCategoryId { get; set; }

        public EduCategory EduCategory { get; set; }

        // Navigation
        public ICollection<EducationPeriodRate> EducationPeriodRates { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}