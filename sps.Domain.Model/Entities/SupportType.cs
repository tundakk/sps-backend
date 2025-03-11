namespace sps.Domain.Model.Entities
{

/// <summary>
/// SupportType er hvilken type af støtteform der er tale om.
/// </summary>
    public class SupportType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } //Interne støttetimer, Interne støttetimer (inkl. opkvalificering og supervision), Interne støttetimer (instruktion), Interne støttetimer (ekstern konsulent), Interne støttetimer (PAU læse-skrive), Interne støttetimer (PAU andet)

        // Navigation
        public ICollection<SpsaCase> SpsaCases { get; set; }

        public ICollection<TeacherPayment> TeacherPayments { get; set; }
        public ICollection<StudentPayment> StudentPayments { get; set; }
    }
}