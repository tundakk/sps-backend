using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.Dtos.SpsaCase;

namespace sps.Domain.Model.Dtos.TeacherPayment
{
    /// <summary>
    /// Basic DTO for teacher payment data
    /// </summary>
    public class TeacherPaymentDto
    {
        /// <summary>
        /// The unique identifier for the payment
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The date of the payment
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The payment amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Optional external voucher reference number
        /// </summary>
        public string ExternalVoucherNumber { get; set; }

        /// <summary>
        /// The name of the support type this payment is for
        /// </summary>
        public string SupportTypeName { get; set; }

        /// <summary>
        /// Number of cases covered by this payment
        /// </summary>
        public int CaseCount { get; set; }

        /// <summary>
        /// Total hours of support covered by this payment
        /// </summary>
        public int TotalHours { get; set; }
    }

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
        /// Optional comment about the payment
        /// </summary>
        [StringLength(500)]
        public string Comment { get; set; }

        /// <summary>
        /// External voucher reference number
        /// </summary>
        [StringLength(50)]
        public string ExternalVoucherNumber { get; set; }

        /// <summary>
        /// The ID of the support type for this payment
        /// </summary>
        public Guid? SupportTypeId { get; set; }

        /// <summary>
        /// IDs of the SPSA cases this payment covers
        /// </summary>
        public List<Guid> SpsaCaseIds { get; set; } = new();
    }

    /// <summary>
    /// DTO for updating an existing teacher payment
    /// </summary>
    public class UpdateTeacherPaymentDto
    {
        /// <summary>
        /// The unique identifier of the payment
        /// </summary>
        [Required]
        public Guid Id { get; set; }

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
        /// Optional comment about the payment
        /// </summary>
        [StringLength(500)]
        public string Comment { get; set; }

        /// <summary>
        /// External voucher reference number
        /// </summary>
        [StringLength(50)]
        public string ExternalVoucherNumber { get; set; }

        /// <summary>
        /// The ID of the support type
        /// </summary>
        public Guid? SupportTypeId { get; set; }

        /// <summary>
        /// IDs of the SPSA cases this payment covers
        /// </summary>
        public List<Guid> SpsaCaseIds { get; set; } = new();
    }

    /// <summary>
    /// Detailed DTO for teacher payment including related data
    /// </summary>
    public class TeacherPaymentDetailDto
    {
        /// <summary>
        /// Basic payment information
        /// </summary>
        public TeacherPaymentDto BasicInfo { get; set; }

        /// <summary>
        /// Optional comment about the payment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Cases covered by this payment
        /// </summary>
        public List<SpsaCaseSummaryDto> Cases { get; set; }

        /// <summary>
        /// Hours and amounts by support type
        /// </summary>
        public List<SupportTypeHours> HoursByType { get; set; }

        public TeacherPaymentDetailDto()
        {
            Cases = new List<SpsaCaseSummaryDto>();
            HoursByType = new List<SupportTypeHours>();
        }
    }

    /// <summary>
    /// Hours and payment statistics by support type
    /// </summary>
    public class SupportTypeHours
    {
        /// <summary>
        /// Name of the support type
        /// </summary>
        public string SupportTypeName { get; set; }

        /// <summary>
        /// Total hours sought for this type
        /// </summary>
        public int HoursSought { get; set; }

        /// <summary>
        /// Total hours spent on this type
        /// </summary>
        public int HoursSpent { get; set; }

        /// <summary>
        /// Total amount paid for this type
        /// </summary>
        public decimal Amount { get; set; }
    }
}