namespace sps.Domain.Model.Entities
{
    public class EduStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; } // Afbrudt, Aktiv, Gennemført, Inaktiv-SPS, Orlov, Syg

        // Navigation
        public ICollection<SpsaCase> SpsaCases { get; set; }
    }
}