using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using Mapster;
using sps.Domain.Model.ValueObjects;
using sps.Domain.Model.Responses;
using sps.Domain.Model.Entities;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing student payments
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentPaymentsController : BaseController<StudentPaymentsController>
    {
        private readonly IStudentPaymentService _studentPaymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentPaymentsController"/> class.
        /// </summary>
        /// <param name="studentPaymentService">The student payment service.</param>
        /// <param name="logger">The logger.</param>
        public StudentPaymentsController(IStudentPaymentService studentPaymentService, ILogger<StudentPaymentsController> logger)
            : base(logger)
        {
            _studentPaymentService = studentPaymentService;
        }

        /// <summary>
        /// Gets all student payments with basic information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _studentPaymentService.GetAllAsync();
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets detailed information about a specific student payment.
        /// </summary>
        /// <param name="id">The ID of the payment to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _studentPaymentService.GetByIdAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new student payment.
        /// </summary>
        /// <param name="model">The payment details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] StudentPaymentModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.AccountNumber?.Value))
                {
                    model.AccountNumber = new SensitiveString(model.AccountNumber.Value);
                }
                
                // Process comments if any are provided
                if (model.Comments?.Count > 0)
                {
                    foreach (var comment in model.Comments)
                    {
                        if (!string.IsNullOrEmpty(comment.CommentText?.Value))
                        {
                            comment.CommentText = new SensitiveString(comment.CommentText.Value);
                        }
                    }
                }
                
                var response = await _studentPaymentService.InsertAsync(model);
                return CreatedResponse(response, nameof(GetByIdAsync), 
                    new { id = response.Data?.Id ?? Guid.Empty });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing student payment.
        /// </summary>
        /// <param name="id">The ID of the payment to update.</param>
        /// <param name="model">The updated payment details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] StudentPaymentModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                if (!string.IsNullOrEmpty(model.AccountNumber?.Value))
                {
                    model.AccountNumber = new SensitiveString(model.AccountNumber.Value);
                }
                
                // Process comments if any are provided
                if (model.Comments?.Count > 0)
                {
                    foreach (var comment in model.Comments)
                    {
                        if (!string.IsNullOrEmpty(comment.CommentText?.Value))
                        {
                            comment.CommentText = new SensitiveString(comment.CommentText.Value);
                        }
                    }
                }
                
                var response = await _studentPaymentService.UpdateAsync(model);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes a student payment.
        /// </summary>
        /// <param name="id">The ID of the payment to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _studentPaymentService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all cases covered by a specific payment.
        /// </summary>
        /// <param name="id">The ID of the payment.</param>
        [HttpGet("{id}/cases")]
        public async Task<IActionResult> GetCasesAsync(Guid id)
        {
            try
            {
                var response = await _studentPaymentService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var payment = response.Data;
                return Ok(payment.SpsaCases);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets hours summary by support type for a specific payment.
        /// </summary>
        /// <param name="id">The ID of the payment.</param>
        [HttpGet("{id}/hours-by-type")]
        public async Task<IActionResult> GetHoursByTypeAsync(Guid id)
        {
            try
            {
                var response = await _studentPaymentService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                // This endpoint may need to be reimplemented based on your model structure
                return Ok(new { message = "This endpoint may need to be reimplemented based on your model structure" });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        
        /// <summary>
        /// Gets all comments for a specific payment.
        /// </summary>
        /// <param name="id">The ID of the payment.</param>
        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsAsync(Guid id)
        {
            try
            {
                var response = await _studentPaymentService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var payment = response.Data;
                return Ok(payment.Comments);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        
        /// <summary>
        /// Adds a comment to an existing payment.
        /// </summary>
        /// <param name="id">The ID of the payment.</param>
        /// <param name="comment">The comment to add.</param>
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddCommentAsync(Guid id, [FromBody] CommentModel comment)
        {
            try
            {
                var response = await _studentPaymentService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var payment = response.Data;
                
                if (!string.IsNullOrEmpty(comment.CommentText?.Value))
                {
                    comment.CommentText = new SensitiveString(comment.CommentText.Value);
                }
                
                comment.EntityType = "StudentPayment";
                comment.EntityId = id;
                comment.CreatedAt = DateTime.UtcNow;
                
                // Create a new payment with updated comments
                var comments = payment.Comments?.Select(c => c.Adapt<CommentModel>()).ToList() ?? new List<CommentModel>();
                comments.Add(comment);
                
                var updatedPayment = new StudentPaymentModel
                {
                    Id = payment.Id,
                    Date = payment.Date,
                    AccountNumber = payment.AccountNumber,
                    Amount = payment.Amount,
                    ExternalVoucherNumber = payment.ExternalVoucherNumber,
                    SupportTypeId = payment.SupportTypeId,
                    Comments = comments.Select(c => c.Adapt<Comment>()).ToList()
                };
                
                var updateResponse = await _studentPaymentService.UpdateAsync(updatedPayment);
                return ProcessResponse(updateResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}