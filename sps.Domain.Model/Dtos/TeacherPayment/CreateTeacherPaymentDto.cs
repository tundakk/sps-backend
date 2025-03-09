using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos.TeacherPayment
{
    /// <summary>
    /// DTO for creating a new teacher payment
    /// </summary>
    public class CreateTeacherPaymentDto
    {
        /// <summary>
        /// The date of the payment
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// The payment amount
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Comments about the payment
        /// </summary>
        public List<string> Comments { get; set; } = new();

        /// <summary>
        /// External voucher reference number
        /// </summary>
        [MaxLength(50)]
        public string ExternalVoucherNumber { get; set; }

        /// <summary>
        /// The ID of the support type for this payment
        /// </summary>
        public Guid? SupportTypeId { get; set; }

        /// <summary>
        /// IDs of the SPSA cases this payment covers
        /// </summary>
        [Required]
        public List<Guid> SpsaCaseIds { get; set; } = new();
    }
}