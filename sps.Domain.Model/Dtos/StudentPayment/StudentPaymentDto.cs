// using System;
// using System.Collections.Generic;

// namespace sps.Domain.Model.Dtos.StudentPayment
// {
//     /// <summary>
//     /// Basic DTO for student payment data used in list views
//     /// </summary>
//     public class StudentPaymentDto
//     {
//         /// <summary>
//         /// The unique identifier for the payment
//         /// </summary>
//         public Guid Id { get; set; }

//         /// <summary>
//         /// The date of the payment
//         /// </summary>
//         public DateTime Date { get; set; }

//         /// <summary>
//         /// The payment amount
//         /// </summary>
//         public decimal Amount { get; set; }

//         /// <summary>
//         /// The student's bank account number (masked)
//         /// </summary>
//         public string AccountNumber { get; set; }

//         /// <summary>
//         /// Comments about the payment
//         /// </summary>
//         public List<StudentPaymentCommentDto> Comments { get; set; } = new();

//         /// <summary>
//         /// External voucher reference number
//         /// </summary>
//         public string ExternalVoucherNumber { get; set; }

//         /// <summary>
//         /// The name of the support type for this payment
//         /// </summary>
//         public string SupportTypeName { get; set; }

//         /// <summary>
//         /// Number of cases covered by this payment
//         /// </summary>
//         public int CaseCount { get; set; }
//     }
// }