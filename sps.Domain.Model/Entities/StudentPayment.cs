namespace sps.Domain.Model.Entities
{
    
   public class StudentPayment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string AccountNumber { get; set; }
    public string Comment { get; set; }
    public decimal Amount { get; set; }
    public string ExternalVoucherNumber { get; set; }
    
    // FK
    public int? SupportTypeId { get; set; }
    public SupportType SupportType { get; set; }
    
    // Navigation
    public ICollection<SpsaCase> SpsaCases { get; set; }
}
}