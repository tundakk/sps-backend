using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.Entities;
using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Models
{
    public class StudentPaymentModel
    {
        public StudentPaymentModel()
        {
            SpsaCases = new HashSet<SpsaCaseModel>();
            Comments = new HashSet<Comment>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public required SensitiveString AccountNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [MaxLength(50)]
        public string? ExternalVoucherNumber { get; set; }
        
        public string? VoucherText { get; set; }
        public string? CompleteVoucherText { get; set; }
        
        public Guid? SupportTypeId { get; set; }
        public SupportTypeModel? SupportType { get; set; }
        
        public ICollection<SpsaCaseModel> SpsaCases { get; init; }
        public ICollection<Comment> Comments { get; init; }
    }
}