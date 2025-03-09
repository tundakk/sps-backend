using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Models
{
    public class TeacherPaymentModel
    {
        public TeacherPaymentModel()
        {
            SpsaCases = new HashSet<SpsaCaseModel>();
            Comments = new HashSet<TeacherPaymentCommentModel>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [MaxLength(50)]
        public string? ExternalVoucherNumber { get; set; }

        public string? VoucherText { get; set; }
        public string? CompleteVoucherText { get; set; }

        public Guid? SupportTypeId { get; set; }
        public SupportTypeModel? SupportType { get; set; }

        public ICollection<SpsaCaseModel> SpsaCases { get; init; }
        public ICollection<TeacherPaymentCommentModel> Comments { get; init; }
    }
}