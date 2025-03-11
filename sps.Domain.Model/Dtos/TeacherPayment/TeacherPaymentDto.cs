// using System;
// using System.Collections.Generic;

// namespace sps.Domain.Model.Dtos.TeacherPayment
// {
//     /// <summary>
//     /// Basic DTO for teacher payment data used in list views
//     /// </summary>
//     public class TeacherPaymentDto
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
//         /// Comments about the payment
//         /// </summary>
//         public List<TeacherPaymentCommentDto> Comments { get; set; } = new();

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