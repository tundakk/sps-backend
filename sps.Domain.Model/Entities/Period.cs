namespace sps.Domain.Model.Entities
{
    using System;

   public class Period
{
    // E.g. "E24","F25" etc.
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Navigation
    public ICollection<EducationPeriodRate> EducationPeriodRates { get; set; }
    public ICollection<SpsaCase> SpsaCases { get; set; }
    public ICollection<Student> Students { get; set; }
}
}