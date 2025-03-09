namespace sps.Domain.Model.Entities
{
    public class Diagnosis
    {
        public Guid Id { get; set; }
        public string Name { get; set; } // Andet, Bevægehandicap, Generelle indlæringsvanskeligheder, Hørenedsættelse, Kronisk eller langvarig sygdom, Læse- skrivevanskeligheder, Matematikvanskeligheder, Neurologisk betinget funktionsnedsættelse ,Psykisk funktionsnedsættelse el. udviklingsforstyrrelse, Sprog- og talevanskeligheder, Synsnedsættelse

        // Navigation
        public ICollection<SpsaCase> SpsaCases { get; set; }
    }
}