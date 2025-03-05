namespace sps.Domain.Model.Entities
{
    using System;

    public class Period
    {
        public Guid Id { get; set; }
        public string Name { get; set; } // E.g. "E24","F25" etc. "E" for Efterår, "F" for Forår

        // Navigation
        public ICollection<EducationPeriodRate> EducationPeriodRates { get; set; }

        public ICollection<SpsaCase> SpsaCases { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}