using System;
using System.Collections.Generic;
using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Entities
{
    public class StudentPayment
    {
        public StudentPayment()
        {
            SpsaCases = new HashSet<SpsaCase>();
            Comments = new HashSet<StudentPaymentComment>();
        }

        public Guid Id { get; set; }
        public required DateTime Date { get; set; }
        public required SensitiveString AccountNumber { get; set; }
        public required decimal Amount { get; set; }
        public string? ExternalVoucherNumber { get; set; }
        public string? VoucherText { get; set; }
        public string? CompleteVoucherText { get; set; }
        
        public Guid? SupportTypeId { get; set; }
        public SupportType? SupportType { get; set; }
        
        public ICollection<SpsaCase> SpsaCases { get; init; }
        public ICollection<StudentPaymentComment> Comments { get; init; }
    }
}