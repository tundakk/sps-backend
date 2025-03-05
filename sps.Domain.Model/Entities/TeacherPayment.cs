using System;
using System.Collections.Generic;
using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Entities
{
    public class TeacherPayment
    {
        public TeacherPayment()
        {
            SpsaCases = new HashSet<SpsaCase>();
        }

        public Guid Id { get; set; }
        public required DateTime Date { get; set; }
        public required decimal Amount { get; set; }
        public SensitiveString? Comment { get; set; }
        public string? ExternalVoucherNumber { get; set; }
        
        public Guid? SupportTypeId { get; set; }
        public SupportType? SupportType { get; set; }
        
        public ICollection<SpsaCase> SpsaCases { get; init; }
    }
}