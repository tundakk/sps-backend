using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.Dtos.SpsaCase;

namespace sps.Domain.Model.Dtos.StudentPayment
{
    /// <summary>
    /// Basic DTO for student payment data
    /// </summary>
    public class StudentPaymentDto
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
        /// The external voucher/reference number
        /// </summary>
        public string? ExternalVoucherNumber { get; set; }
        
        /// <summary>
        /// The type of support this payment is for
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
    /// DTO for creating a new student payment
    /// </summary>
    public class CreateStudentPaymentDto
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
        /// The student's bank account number
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{4}-\d{10}$", ErrorMessage = "Account number must be in format RRRR-XXXXXXXXXX")]
        public string AccountNumber { get; set; }
        
        /// <summary>
        /// Optional comment about the payment
        /// </summary>
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
        public string? Comment { get; set; }
        
        /// <summary>
        /// External voucher/reference number
        /// </summary>
        [StringLength(50, ErrorMessage = "External voucher number cannot exceed 50 characters")]
        public string? ExternalVoucherNumber { get; set; }
        
        /// <summary>
        /// The ID of the support type
        /// </summary>
        public Guid? SupportTypeId { get; set; }
        
        /// <summary>
        /// The IDs of the SPSA cases covered by this payment
        /// </summary>
        [Required]
        [MinLength(1, ErrorMessage = "At least one case must be specified")]
        public List<Guid> SpsaCaseIds { get; set; } = new();
    }

    /// <summary>
    /// DTO for updating an existing student payment
    /// </summary>
    public class UpdateStudentPaymentDto
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
        /// The student's bank account number
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{4}-\d{10}$", ErrorMessage = "Account number must be in format RRRR-XXXXXXXXXX")]
        public string AccountNumber { get; set; }
        
        /// <summary>
        /// Optional comment about the payment
        /// </summary>
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
        public string? Comment { get; set; }
        
        /// <summary>
        /// External voucher/reference number
        /// </summary>
        [StringLength(50, ErrorMessage = "External voucher number cannot exceed 50 characters")]
        public string? ExternalVoucherNumber { get; set; }
        
        /// <summary>
        /// The ID of the support type
        /// </summary>
        public Guid? SupportTypeId { get; set; }
        
        /// <summary>
        /// The IDs of the SPSA cases covered by this payment
        /// </summary>
        [Required]
        [MinLength(1, ErrorMessage = "At least one case must be specified")]
        public List<Guid> SpsaCaseIds { get; set; } = new();
    }

    /// <summary>
    /// Detailed DTO for student payment including related data
    /// </summary>
    public class StudentPaymentDetailDto
    {
        /// <summary>
        /// Basic payment information
        /// </summary>
        public StudentPaymentDto BasicInfo { get; set; }
        
        /// <summary>
        /// Comment about the payment (if any)
        /// </summary>
        public string? Comment { get; set; }
        
        /// <summary>
        /// List of cases covered by this payment
        /// </summary>
        public List<SpsaCaseSummaryDto> Cases { get; set; }
        
        /// <summary>
        /// Summary of hours by support type
        /// </summary>
        public List<SupportTypeHours> HoursByType { get; set; }
        
        public StudentPaymentDetailDto()
        {
            Cases = new List<SpsaCaseSummaryDto>();
            HoursByType = new List<SupportTypeHours>();
        }
    }

    /// <summary>
    /// Summary of hours by support type
    /// </summary>
    public class SupportTypeHours
    {
        /// <summary>
        /// The name of the support type
        /// </summary>
        public string SupportTypeName { get; set; }
        
        /// <summary>
        /// Hours sought for this type of support
        /// </summary>
        public int HoursSought { get; set; }
        
        /// <summary>
        /// Hours actually spent on this type of support
        /// </summary>
        public int HoursSpent { get; set; }
        
        /// <summary>
        /// Amount paid for this type of support
        /// </summary>
        public decimal Amount { get; set; }
    }
}