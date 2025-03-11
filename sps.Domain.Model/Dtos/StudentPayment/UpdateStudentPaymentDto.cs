// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;

// namespace sps.Domain.Model.Dtos.StudentPayment
// {
//     /// <summary>
//     /// DTO for updating an existing student payment
//     /// </summary>
//     public class UpdateStudentPaymentDto
//     {
//         /// <summary>
//         /// The unique identifier for the payment
//         /// </summary>
//         [Required]
//         public Guid Id { get; set; }

//         /// <summary>
//         /// The date of the payment
//         /// </summary>
//         [Required]
//         public DateTime Date { get; set; }

//         /// <summary>
//         /// The payment amount
//         /// </summary>
//         [Required]
//         [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
//         public decimal Amount { get; set; }

//         /// <summary>
//         /// The student's bank account number
//         /// </summary>
//         [Required]
//         [RegularExpression(@"^\d{4}-\d{10}$", ErrorMessage = "Account number must be in the format 'RRRR-NNNNNNNNNN'")]
//         public string AccountNumber { get; set; }

//         /// <summary>
//         /// Comments about the payment
//         /// </summary>
//         public List<string> Comments { get; set; } = new();

//         /// <summary>
//         /// External voucher reference number
//         /// </summary>
//         [MaxLength(50)]
//         public string ExternalVoucherNumber { get; set; }

//         /// <summary>
//         /// The ID of the support type
//         /// </summary>
//         public Guid? SupportTypeId { get; set; }

//         /// <summary>
//         /// The IDs of the SPSA cases covered by this payment
//         /// </summary>
//         [Required]
//         public List<Guid> SpsaCaseIds { get; set; } = new();
//     }
// }