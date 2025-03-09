namespace sps.Domain.Model.Entities
{
    public class EduCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }   // e.g. Professionsbachelor, Erhvervsakademiuddannelse, Ungdomsuddannelse

        // Navigation
        public ICollection<Education> Educations { get; set; }

        public ICollection<SpsaCase> SpsaCases { get; set; }
    }
}