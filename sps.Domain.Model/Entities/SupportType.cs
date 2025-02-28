namespace sps.Domain.Model.Entities
{
    
   public class SupportType
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Navigation
    public ICollection<SpsaCase> SpsaCases { get; set; }
    public ICollection<TeacherPayment> TeacherPayments { get; set; }
    public ICollection<StudentPayment> StudentPayments { get; set; }
}
}